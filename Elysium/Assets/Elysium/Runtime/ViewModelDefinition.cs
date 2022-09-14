using System.Collections.Generic;
using System.ComponentModel;
using Elysium.Components;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Elysium
{
    /// <summary>
    /// Acts as a bridge to define a view model.
    /// </summary>
    [PublicAPI, ExecuteAlways, DisallowMultipleComponent, DefaultExecutionOrder(-10000)]
    public class ViewModelDefinition : NotifiableBehaviour
    {
        private object? _viewModel;
        private readonly List<ComponentPropertyBinding> _bindings = new();

        [field: SerializeField]
        internal Object? ViewModelObject { get; private set; }
        
        /// <summary>
        /// The View Model in this definition.
        /// </summary>
        public object? ViewModel
        {
            // We check the unity object reference first to ensure that the ViewModel hasn't been destroyed.
            get => ViewModelObject != null ? ViewModelObject : _viewModel;
            set
            {
                if (!SetField(ref _viewModel, value))
                    return;

                ViewModelObject = value switch
                {
                    // If we have successfully set the variable, update the ViewModelObject so it can get serialized 
                    // properly.
                    Object valueAsUnityObject => valueAsUnityObject,
                    // However if the value is null, we also want to make sure the serialized field is updated
                    null => null,
                    _ => ViewModelObject
                };
            }
        }

        private void Start()
        {
            ElysiumBindings.OnBindingRegistered += BindingRegistered;
            ElysiumBindings.OnBindingUnregistered += BindingUnregistered;
        }

        private void OnDestroy()
        {
            ElysiumBindings.OnBindingUnregistered -= BindingUnregistered;
            ElysiumBindings.OnBindingRegistered -= BindingRegistered; 
        }
        
        private void BindingRegistered(ViewModelDefinition definition, ComponentPropertyBinding binding)
        {
            // Make sure the binding is relevant to us.
            if (definition != this)
                return;
            
            // Add the binding to our internal list.
            _bindings.Add(binding);
        }

        private void BindingUnregistered(ViewModelDefinition definition, ComponentPropertyBinding binding)
        {
            // Make sure the binding is relevant to us.
            if (definition != this)
                return;
            
            // Remove the binding from our internal list
            _bindings.Remove(binding);
        }

        protected override void OnPropertyChanging(string? propertyName = null)
        {
            // Ensure we're working with the right property.
            if (propertyName != nameof(ViewModel))
                return;

            // We can only unsubscribe from property changes if the view model supports it.
            if (ViewModel is not INotifyPropertyChanged propertyChanger)
                return;

            // Unsubscribe us from the property changed event.
            propertyChanger.PropertyChanged -= ViewModel_PropertyChanged;
        }

        protected override void OnPropertyChanged(string? propertyName = null)
        {
            // Ensure we're working with the right property.
            if (propertyName != nameof(ViewModel))
                return;
            
            // We can only listen into property changes if the view model supports it.
            if (ViewModel is not INotifyPropertyChanged propertyChanger)
                return;

            // Subscribe us to the property changed event.
            propertyChanger.PropertyChanged += ViewModel_PropertyChanged;
        }
        
        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ViewModel == null)
                return;
            
            foreach (var binding in _bindings)
            {
                // Skip over the bindings that don't match this property update.
                if (binding.Name != e.PropertyName)
                    continue;
                
                binding.OnValueChanged(ViewModel, e.PropertyName);
            }
        }
    }
}
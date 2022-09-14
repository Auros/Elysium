using System.ComponentModel;
using Elysium.Components;
using JetBrains.Annotations;
using UnityEngine;

namespace Elysium
{
    /// <summary>
    /// Acts as a bridge to define a view model.
    /// </summary>
    [PublicAPI, ExecuteAlways, DisallowMultipleComponent]
    public class ViewModelDefinition : NotifiableBehaviour
    {
        private object? _viewModel;

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
        
        private void OnEnable()
        {
            // TODO: Subscribe to static binding UI system
        }

        private void OnDisable()
        {
            // TODO: Unsubscribe from static binding UI SYSTEM
        }

        private void OnTransformChildrenChanged()
        {
            print("child changed");
            // TODO: Check registered bindings to ensure that we still own them.
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
            print($"Property Changed: {e.PropertyName}");
        }
    }
}
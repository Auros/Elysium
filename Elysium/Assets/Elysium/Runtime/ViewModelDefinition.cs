using Elysium.Components;
using JetBrains.Annotations;
using UnityEngine;

namespace Elysium
{
    /// <summary>
    /// Acts as a bridge to define a view model.
    /// </summary>
    [PublicAPI, DisallowMultipleComponent]
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
            get => _viewModel;
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
    }
}
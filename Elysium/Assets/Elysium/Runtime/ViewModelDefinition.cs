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
        [SerializeField]
        private object? _viewModel;

        /// <summary>
        /// The View Model in this definition.
        /// </summary>
        public object? ViewModel
        {
            get => _viewModel;
            set => SetField(ref _viewModel, value);
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
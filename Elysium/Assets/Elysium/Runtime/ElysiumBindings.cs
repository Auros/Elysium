using System;
using Elysium.Components;
using UnityEngine;

namespace Elysium
{
    public static class ElysiumBindings
    {
        public static event Action<ViewModelDefinition, ComponentPropertyBinding>? OnBindingRegistered; 
        public static event Action<ViewModelDefinition, ComponentPropertyBinding>? OnBindingUnregistered;

        public static void Add(ViewModelDefinition definition, ComponentPropertyBinding binding)
            => OnBindingRegistered?.Invoke(definition, binding);
        
        public static void Remove(ViewModelDefinition definition, ComponentPropertyBinding binding)
            => OnBindingUnregistered?.Invoke(definition, binding);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            // Forcibly unsubscribe everything when the domain gets reloaded
            OnBindingUnregistered = null;
            OnBindingRegistered = null;
        }
    }
}
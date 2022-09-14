using UnityEngine;

namespace Elysium.Playground
{
    public class ViewModelAssigner : MonoBehaviour
    {
        [SerializeField]
        private ViewModelDefinition _viewModelDefinition;
        
        private void Awake()
        {
            _viewModelDefinition.ViewModel = new NormalViewModel();
        }
    }
}
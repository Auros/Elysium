using PropertyChanged.SourceGenerator;
using UnityEngine;

namespace Elysium.Playground
{
    [ExecuteAlways]
    public partial class BasicViewModel : MonoBehaviour
    {
        [Notify] private string _name;
        [SerializeField] private int _lastTime;
        
        private void Start()
        {
            Name = "hello";
        }

        private void Update()
        {
            var time = (int)Time.realtimeSinceStartup;
            if (_lastTime >= time)
                return;

            Name = time.ToString();
            _lastTime = time;
        }
    }
}
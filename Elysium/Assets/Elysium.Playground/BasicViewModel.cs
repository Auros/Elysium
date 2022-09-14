using PropertyChanged.SourceGenerator;
using UnityEngine;

namespace Elysium.Playground
{
    [ExecuteAlways]
    public partial class BasicViewModel : MonoBehaviour
    {
        [Notify] private string _fieldValue = string.Empty;
        [Notify] private string _timeSinceStart = string.Empty;
        [SerializeField] private int _lastTime;
        
        private void Start()
        {
            _lastTime = 0;
            FieldValue = "yo";
            TimeSinceStart = "hello";
        }

        private void Update()
        {
            var time = (int)Time.realtimeSinceStartup;
            if (_lastTime >= time)
                return;

            TimeSinceStart = time.ToString();
            _lastTime = time;
        }
    }
}
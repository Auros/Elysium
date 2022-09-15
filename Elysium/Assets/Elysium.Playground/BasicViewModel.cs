using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using JetBrains.Annotations;
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

        [PublicAPI]
        public ICommand CoolCommand { get; private set; }

        [PublicAPI]
        public ObservableCollection<TestObj> TestCollection { get; set; } = new()
        {
            new TestObj("A"),
            new TestObj("B"),
            new TestObj("C"),
        };

        private void Start()
        {
            _lastTime = 0;
            FieldValue = "";
            TimeSinceStart = "0";
            //CoolCommand = new TestCommand(() => print("Command Invoked"), () => FieldValue.Length != 0);
        }

        private void Update()
        {
            var time = (int)Time.realtimeSinceStartup;
            if (_lastTime >= time)
                return;

            TimeSinceStart = time.ToString();
            _lastTime = time;
        }

        public void Add()
        {
            TestCollection.Add(new TestObj(Random.Range(0, 100).ToString()));
        }

        public void Remove()
        {
            var item = TestCollection.LastOrDefault();
            if (item is null)
                return;
            
            TestCollection.Remove(item);
        }
        
        public partial class TestObj
        {
            [Notify] private string _name;

            public TestObj(string name)
            {
                Name = name;
            }
        }
    }
}
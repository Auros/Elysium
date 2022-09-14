using System;
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
        
        private void Start()
        {
            _lastTime = 0;
            FieldValue = "";
            TimeSinceStart = "0";
            CoolCommand = new TestCommand(() => print("Command Invoked"), () => FieldValue.Length != 0);
        }

        private void Update()
        {
            var time = (int)Time.realtimeSinceStartup;
            if (_lastTime >= time)
                return;

            TimeSinceStart = time.ToString();
            _lastTime = time;
        }

        public void SendData()
        {
            print("sent data");
        }

        private class TestCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;
            public event EventHandler CanExecuteChanged;

            public TestCommand(Action execute, Func<bool> canExecute)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter) => _canExecute.Invoke();

            public void Execute(object parameter) => _execute?.Invoke();
        }
    }
}
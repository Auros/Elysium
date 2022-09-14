using PropertyChanged.SourceGenerator;
using UnityEngine;

namespace Elysium.Playground
{
    public partial class BasicViewModel : MonoBehaviour
    {
        [Notify]
        private string _name;

        private void Start()
        {
            Name = "hello";
        }
    }
}
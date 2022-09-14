using Elysium.Components;
using TMPro;
using UnityEngine;

namespace Elysium.TextMeshPro
{
    [RequireComponent(typeof(TMP_Text))]
    public class TMPTextTextPropertyBinding : ComponentPropertyBinding
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public override void OnValueChanged(object host, string propertyName)
        {
            var value = host.GetType().GetProperty(propertyName)?.GetValue(host);
            if (value is not string valueAsString)
                return;

            _text.text = valueAsString;
        }
    }
}
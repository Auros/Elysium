using Elysium.Components;
using TMPro;
using UnityEngine;

namespace Elysium.TextMeshPro
{
    [RequireComponent(typeof(TMP_Text))]
    public class TMP_TextTextPropertyBinding : ComponentPropertyBinding
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public override void OnValueChanged(object host, string propertyName)
        {
            var value = host.GetType().GetProperty(propertyName)?.GetValue(host);
            _text.text = value?.ToString();
        }
    }
}
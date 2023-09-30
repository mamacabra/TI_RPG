using TMPro;
using UnityEngine;

namespace Combat
{
    public class StatusBar : MonoBehaviour, ICharacterObserver
    {
        [Header("Status")]
        public TMP_Text statusText;

        private void Awake()
        {
            statusText = GetComponentInChildren<TMP_Text>();
            statusText.text = "";
        }

        public void OnCharacterCreated(Character character) {}

        public void OnCharacterUpdated(Character character)
        {
            if (!statusText) return;

            statusText.text = "";
            foreach (StatusType status in character.Status)
            {
                statusText.text += status + "\n";
            }
        }
    }
}

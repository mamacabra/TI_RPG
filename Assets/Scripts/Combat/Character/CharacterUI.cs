using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class CharacterUI : MonoBehaviour, ICharacterObserver
    {
        [Header("Counters")]
        public Text healthBarCount;
        public Text actionPointsCount;

        [Header("Passive")]
        public TMP_Text characterPassiveName;

        [Header("Status")]
        public TMP_Text statusText;

        [Header("Slider")]
        public Slider healthBarSlider;

        public void OnCharacterCreated(Character character)
        {
            SetupHealthBar(character.maxHealth);
            ClearStatusText();
            OnCharacterUpdated(character);
        }

        public void OnCharacterUpdated(Character character)
        {
            SetHealthBar(character.Health);
            SetActionPoints(character.ActionPoints);
            SetPassiveName(character.Passive);
            SetStatusText(character.Status);
        }

        private void SetupHealthBar(int maxHealth)
        {
            if (healthBarSlider)
                healthBarSlider.maxValue = maxHealth;
        }

        private void SetHealthBar(int health)
        {
            if (healthBarSlider)
                healthBarSlider.value = health;

            if (healthBarCount)
                healthBarCount.text = "VIDA: " + health;
        }

        private void SetActionPoints(int actionPoints)
        {
            if (actionPointsCount)
                actionPointsCount.text = "" + actionPoints;
        }

        private void SetPassiveName(PassiveType passive = PassiveType.None)
        {
            if (!characterPassiveName) return;

            characterPassiveName.text = passive is not PassiveType.None
                ? passive.ToString()
                : "";
        }

        private void ClearStatusText()
        {
            if (!statusText) return;
            statusText.text = "";
        }

        private void SetStatusText(List<StatusData> statusList)
        {
            if (!statusText) return;

            ClearStatusText();
            foreach (StatusData status in statusList)
            {
                statusText.text += $"{status.type} ({status.duration})\n";
            }
        }
    }
}

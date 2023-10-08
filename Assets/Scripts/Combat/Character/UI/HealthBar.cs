using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class HealthBar : MonoBehaviour, ICharacterObserver
    {
        [Header("Slider")]
        public Slider healthBarSlider;

        [Header("Counters")]
        public Text healthBarCount;
        public Text actionPointsCount;
        public TMP_Text characterPassiveName;

        public void OnCharacterCreated(Character character)
        {
            SetupHealthBar(character.maxHealth);
            OnCharacterUpdated(character);
        }

        public void OnCharacterUpdated(Character character)
        {
            SetHealthBar(character.Health);
            SetActionPoints(character.ActionPoints);
            SetPassiveName(character.Passive);
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
                healthBarCount.text = "HP: " + health;
        }

        private void SetActionPoints(int actionPoints)
        {
            if (actionPointsCount)
                actionPointsCount.text = "AP: " + actionPoints;
        }

        private void SetPassiveName(PassiveType passive = PassiveType.None)
        {
            if (!characterPassiveName) return;

            characterPassiveName.text = passive is not PassiveType.None
                ? passive.ToString()
                : "";
        }
    }
}

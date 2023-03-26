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

        public void OnCharacterCreated(Character character)
        {
            healthBarSlider.maxValue = Character.MaxHealth;
            OnCharacterUpdated(character);
        }

        public void OnCharacterUpdated(Character character)
        {
            healthBarSlider.value = character.Health;
            healthBarCount.text = "HP: " + character.Health;
            actionPointsCount.text = "AP: " + character.ActionPoints;
        }
    }
}

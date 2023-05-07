using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Member))]
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(DeckCardList))]
    [RequireComponent(typeof(Target))]
    [RequireComponent(typeof(VFXSelected))]
    public class CharacterSetup : MonoBehaviour
    {
        [Header("Character Observers")]
        [SerializeField] private HealthBar healthBar;

        private void Awake()
        {
            SetupBoxCollider();
            SetupCharacter();
        }

        private void SetupBoxCollider()
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.isTrigger = true;
        }

        private void SetupCharacter()
        {
            Character character = GetComponent<Character>();

            if (healthBar) character.observers.Add(healthBar);
            character.observers.Add(CombatManager.Instance);
        }
    }
}

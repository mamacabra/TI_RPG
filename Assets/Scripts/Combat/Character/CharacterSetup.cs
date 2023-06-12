using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Member))]
    [RequireComponent(typeof(Character))]
    [RequireComponent(typeof(Target))]
    [RequireComponent(typeof(CharacterCardSelectedVFX))]
    public class CharacterSetup : MonoBehaviour
    {
        [Header("Character Observers")]
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private CharacterType characterType = CharacterType.Hero;
        [SerializeField] public ItemScriptableObject[] items = new ItemScriptableObject[4];

        private void Start()
        {
            SetupBoxCollider();
            SetupMember();
            SetupCharacter();
        }

        private void SetupBoxCollider()
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.isTrigger = true;
        }

        private void SetupMember()
        {
            Character character = GetComponent<Character>();
            Member member = GetComponent<Member>();
            List<CardScriptableObject> cards = new List<CardScriptableObject>();

            member.Character = character;
            foreach (ItemScriptableObject item in items)
            {
                if (item == null) continue;
                foreach (CardScriptableObject card in item.cards)
                {
                    if (card != null) cards.Add(card);
                }
            }

            if (cards.Count > 0) member.SetupDeck(cards);
        }

        private void SetupCharacter()
        {
            Character character = GetComponent<Character>();
            character.Type = characterType;

            character.Subscribe(healthBar);
            character.Subscribe(CombatManager.Instance);
        }
    }
}

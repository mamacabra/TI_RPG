using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private int characterId;
        [SerializeField] private CharacterType characterType = CharacterType.Hero;
        [SerializeField] private int characterMaxHealth = 10;
        [SerializeField] private int characterMaxActionPoints = 3;
        [SerializeField] public ItemScriptableObject[] items = new ItemScriptableObject[4];

        private void Start()
        {
            GetInventory();
            SetupBoxCollider();
            SetupMember();
            SetupCharacter();
        }

        private void GetInventory()
        {
            if (characterType != CharacterType.Hero) return;

            List<ItemScriptableObject> inventory = Inventory.Storage.LoadHeroInventory(characterId);
            items = new ItemScriptableObject[4];
            int i = 0;
            foreach (var item in inventory.Where(item => item is not null))
            {
                items[i] = item;
                i++;
            }
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
            character.maxHealth = characterMaxHealth;
            character.maxActionPoints = characterMaxActionPoints;

            character.Subscribe(healthBar);
            character.Subscribe(CombatManager.Instance);
            character.CharacterCreated();
        }
    }
}

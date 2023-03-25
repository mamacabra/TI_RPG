using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public abstract class Character : MonoBehaviour
    {
        public bool isDead => health <= 0;
        public int Health => health;
        public int MaxHealth => maxHealth;
        public int ActionPoints => actionPoints;
        public int MaxActionPoints => maxActionPoints;
        public CharacterType Type => type;

        [Header("Observers")]
        [SerializeField] private HealthBar healthBar;

        [Header("Information")]
        [SerializeField] private int health;
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int actionPoints;
        [SerializeField] private int maxActionPoints = 3;

        [SerializeField] protected CharacterType type;

        public List<Card> hand = new List<Card>();
        public Deck deck;

        protected void CharacterCreated()
        {
            health = maxHealth;
            actionPoints = maxActionPoints;

            healthBar.OnCharacterCreated(this);
            CombatManager.Instance.OnCharacterCreated(this);
        }

        private void CharacterUpdated()
        {
            healthBar.OnCharacterUpdated(this);
            CombatManager.Instance.OnCharacterUpdated(this);
        }

        public void ReceiveDamage(int value = 1)
        {
            health -= value;
            if (health < 0) health = 0;

            CharacterUpdated();
        }

        public void ReceiveHealing(int value = 1)
        {
            health += value;
            if (health > maxHealth) health = maxHealth;

            CharacterUpdated();
        }

        public bool ConsumeActionPoints(int value = 1)
        {
            bool hasEnough = actionPoints >= value;
            if (hasEnough)
            {
                actionPoints -= value;
                if (actionPoints < 0) actionPoints = 0;
                CharacterUpdated();
            }

            return hasEnough;
        }

        public void ResetActionPoints()
        {
            actionPoints = maxActionPoints;
            CharacterUpdated();
        }
    }
}

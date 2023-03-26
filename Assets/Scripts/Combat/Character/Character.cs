using UnityEngine;

namespace Combat
{
    public abstract class Character : MonoBehaviour
    {
        [Header("Observers")]
        [SerializeField] private HealthBar healthBar;

        public bool IsDead => health <= 0;
        public int Health => health;
        public int MaxHealth => maxHealth;
        public int ActionPoints => actionPoints;
        public int MaxActionPoints => maxActionPoints;
        public virtual CharacterType Type { get; }

        [SerializeField] private int health;
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private int actionPoints;
        [SerializeField] private int maxActionPoints = 3;

        private void Start()
        {
            CharacterCreated();
        }

        private void CharacterCreated()
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

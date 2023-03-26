using UnityEngine;

namespace Combat
{
    public abstract class Character : MonoBehaviour
    {
        [Header("Observers")]
        [SerializeField] private HealthBar healthBar;

        public bool IsDead => Health <= 0;

        public int Health { get; private set; }
        public const int MaxHealth = 10;
        public int ActionPoints { get; private set; }
        private int MaxActionPoints = 3;

        public virtual CharacterType Type { get; }

        private void Start()
        {
            CharacterCreated();
        }

        private void CharacterCreated()
        {
            Health = MaxHealth;
            ActionPoints = MaxActionPoints;

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
            Health -= value;
            if (Health < 0) Health = 0;

            CharacterUpdated();
        }

        public void ReceiveHealing(int value = 1)
        {
            Health += value;
            if (Health > MaxHealth) Health = MaxHealth;

            CharacterUpdated();
        }

        public bool ConsumeActionPoints(int value = 1)
        {
            bool hasEnough = ActionPoints >= value;
            if (hasEnough)
            {
                ActionPoints -= value;
                if (ActionPoints < 0) ActionPoints = 0;
                CharacterUpdated();
            }

            return hasEnough;
        }

        public void ResetActionPoints()
        {
            ActionPoints = MaxActionPoints;
            CharacterUpdated();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Combat
{

    public class Character : MonoBehaviour
    {
        public List<ICharacterObserver> observers;

        [Header("Attributes")]
        public CharacterType type = CharacterType.Hero;
        public int Health { get; private set; }
        public const int MaxHealth = 10;
        public int ActionPoints { get; private set; }
        private const int MaxActionPoints = 3;

        public bool IsDead => Health <= 0;

        private void Start()
        {
            CharacterCreated();
        }

        private void CharacterCreated()
        {
            Health = MaxHealth;
            ActionPoints = MaxActionPoints;

            foreach (var observer in observers) observer.OnCharacterCreated(this);
        }

        private void CharacterUpdated()
        {
            foreach (var observer in observers) observer.OnCharacterUpdated(this);
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

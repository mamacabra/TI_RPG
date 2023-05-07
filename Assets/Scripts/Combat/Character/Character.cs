using System.Collections.Generic;
using UnityEngine;

namespace Combat
{

    public class Character : MonoBehaviour
    {
        private List<ICharacterObserver> _observers;

        public CharacterType Type { get; set; }
        public int Health { get; private set; }
        public const int MaxHealth = 10;
        public int ActionPoints { get; private set; }
        private const int MaxActionPoints = 3;

        public bool IsDead => Health <= 0;

        private void Awake()
        {
            _observers = new List<ICharacterObserver>();
        }

        private void Start()
        {
            CharacterCreated();
        }

        public void Subscribe(ICharacterObserver observer)
        {
            if (observer != null)
                _observers.Add(observer);
        }

        private void CharacterCreated()
        {
            Health = MaxHealth;
            ActionPoints = MaxActionPoints;

            foreach (var observer in _observers)
                observer.OnCharacterCreated(this);
        }

        private void CharacterUpdated()
        {
            foreach (var observer in _observers)
                observer.OnCharacterUpdated(this);
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

        public bool HasEnoughActionPoints(int value)
        {
            return ActionPoints >= value;
        }

        public void ConsumeActionPoints(int value = 1)
        {
            ActionPoints -= value;
            if (ActionPoints < 0) ActionPoints = 0;
            CharacterUpdated();
        }

        public void ReceiveActionPoints(int value = 1)
        {
            ActionPoints += value;
            CharacterUpdated();
        }

        public void ResetActionPoints()
        {
            ActionPoints = MaxActionPoints;
            CharacterUpdated();
        }
    }
}

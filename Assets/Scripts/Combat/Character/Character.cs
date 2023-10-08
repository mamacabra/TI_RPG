using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Combat
{
    public class Character : MonoBehaviour
    {
        private List<ICharacterObserver> _observers;

        public CharacterType Type { get; set; }
        public int Health { get; private set; }
        public int maxHealth = 10;
        public int ActionPoints { get; private set; }
        public int maxActionPoints = 3;
        public List<StatusType> Status { get; } = new();
        public PassiveType Passive { get; private set; }

        public bool IsDead => Health <= 0;
        public bool HasActionPoints => ActionPoints > 0;

        private void Awake()
        {
            _observers = new List<ICharacterObserver>();
        }

        public void Subscribe(ICharacterObserver observer)
        {
            if (observer != null)
                _observers.Add(observer);
        }

        public void CharacterCreated()
        {
            Health = maxHealth;
            ActionPoints = maxActionPoints;

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
            if (Health <= 0)
            {
                Health = 0;
                gameObject.SetActive(false);
            }
            Target.ClearClickedTarget();
            CharacterUpdated();
        }

        public void ReceiveHealing(int value = 1)
        {
            Health += value;
            if (Health > maxHealth) Health = maxHealth;

            CharacterUpdated();
        }

        public void ReceiveStatus(StatusType status)
        {
            Status.Add(status);
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
            ActionPoints = maxActionPoints;
            CharacterUpdated();
        }

        public void RandomizePassive()
        {
            if (Type != CharacterType.Hero) return;

            int count = Enum.GetNames(typeof(PassiveType)).Length;
            Passive = (PassiveType) Random.Range(1, count);
        }
    }
}

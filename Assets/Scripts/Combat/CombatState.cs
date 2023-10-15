using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatState : MonoBehaviour
    {
        public static CombatState Instance;

        private CombatStateType _state = CombatStateType.Wait;
        private Queue<CombatStateType> _stateChanges;
        private List<ICombatStateObserver> _observers;

        private void Awake()
        {
            Instance = this;
            _stateChanges = new Queue<CombatStateType>();
            _observers = new List<ICombatStateObserver>();
        }

        private void FixedUpdate()
        {
            while (_stateChanges.Count != 0)
            {
                _state = _stateChanges.Dequeue();
                foreach (var observer in _observers)
                    observer.OnCombatStateChanged(_state);
            }
        }

        public void Subscribe(ICombatStateObserver observer)
        {
            _observers.Add(observer);
            if (_state == CombatStateType.Wait) CheckRequiredObservers();
        }

        public void SetState(CombatStateType newState)
        {
            if (_state is CombatStateType.Defeat or CombatStateType.Victory) return;
            _stateChanges.Enqueue(newState);
        }

        public void NextState()
        {
            switch (_state)
            {
                case CombatStateType.PreparationStage:
                    SetState(CombatStateType.HeroStatus);
                    break;
                case CombatStateType.HeroStatus:
                    SetState(CombatStateType.HeroPassive);
                    break;
                case CombatStateType.HeroPassive:
                    SetState(CombatStateType.HeroTurn);
                    break;
                case CombatStateType.HeroTurn:
                    SetState(CombatStateType.HeroDeckShuffle);
                    break;
                case CombatStateType.HeroDeckShuffle:
                    SetState(CombatStateType.EnemyStatus);
                    break;
                case CombatStateType.EnemyStatus:
                    SetState(CombatStateType.EnemyPassive);
                    break;
                case CombatStateType.EnemyPassive:
                    SetState(CombatStateType.EnemyTurn);
                    break;
                case CombatStateType.EnemyTurn:
                    SetState(CombatStateType.EnemyDeckShuffle);
                    break;
                case CombatStateType.EnemyDeckShuffle:
                    SetState(CombatStateType.HeroStatus);
                    break;
            }
        }

        private void CheckRequiredObservers()
        {
            bool hasCombatCharacterStatus = false;
            bool hasCombatCharacterPassive = false;
            bool hasCombatManager = false;
            bool hasCombatHudController = false;
            bool hasEnemyIa = false;

            foreach (ICombatStateObserver observer in _observers)
            {
                if (typeof(CombatCharacterStatus) == observer.GetType()) hasCombatCharacterStatus = true;
                if (typeof(CombatCharacterPassive) == observer.GetType()) hasCombatCharacterPassive = true;
                if (typeof(CombatManager) == observer.GetType()) hasCombatManager = true;
                if (typeof(CombatHudController) == observer.GetType()) hasCombatHudController = true;
                if (typeof(EnemyIA) == observer.GetType()) hasEnemyIa = true;
            }

            bool hasAllObservers = hasCombatCharacterStatus && hasCombatCharacterPassive && hasCombatManager && hasCombatHudController && hasEnemyIa;
            if (hasAllObservers) SetState(CombatStateType.PreparationStage);
        }
    }
}

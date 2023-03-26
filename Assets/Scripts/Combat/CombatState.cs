using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatState : MonoBehaviour
    {
        public static CombatState Instance;

        [SerializeField] private CombatStateType state = CombatStateType.Wait;
        private List<ICombatStateObserver> observers;
        private readonly Type[] requiredObservers = { typeof(CombatManager), typeof(CombatHudController) };

        private void Awake()
        {
            Instance = this;
            observers = new List<ICombatStateObserver>();
        }

        public void AddObserver(ICombatStateObserver observer)
        {
            observers.Add(observer);
            if (state == CombatStateType.Wait) CheckRequiredObservers();
        }

        public void SetState(CombatStateType newState)
        {
            if (state is CombatStateType.Defeat or CombatStateType.Victory) return;

            state = newState;
            foreach (var observer in observers) observer.OnCombatStateChanged(state);
        }

        public void NextState()
        {
            switch (state)
            {
                case CombatStateType.Start:
                    SetState(CombatStateType.HeroTurn);
                    break;
                case CombatStateType.HeroTurn:
                    SetState(CombatStateType.HeroDeckShuffle);
                    break;
                case CombatStateType.HeroDeckShuffle:
                    SetState(CombatStateType.EnemyTurn);
                    break;
                case CombatStateType.EnemyTurn:
                    SetState(CombatStateType.EnemyDeckShuffle);
                    break;
                case CombatStateType.EnemyDeckShuffle:
                    SetState(CombatStateType.HeroTurn);
                    break;
            }
        }

        private void CheckRequiredObservers()
        {
            bool hasCombatManager = false;
            bool hasCombatHudController = false;
            foreach (ICombatStateObserver observer in observers)
            {
                if (requiredObservers[0] == observer.GetType()) hasCombatManager = true;
                if (requiredObservers[1] == observer.GetType()) hasCombatHudController = true;
            }

            if (hasCombatManager && hasCombatHudController) SetState(CombatStateType.Start);
        }
    }
}

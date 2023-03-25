using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatState : MonoBehaviour
    {
        public static CombatState Instance;

        [SerializeField] private CombatStateType state;
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
            CheckObserversList();
        }

        public void SetState(CombatStateType newState)
        {
            state = newState;
            foreach (var observer in observers) observer.OnCombatStateChanged(state);
        }

        private void CheckObserversList()
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

using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public enum CombatStateType
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Victory,
        Defeat,
    }

    public class CombatState : MonoBehaviour
    {
        public static CombatState Instance;

        [SerializeField] private CombatStateType state;
        private List<ICombatStateObserver> observers;

        private void Awake()
        {
            Instance = this;
            observers = new List<ICombatStateObserver>();
        }

        public void AddObserver(ICombatStateObserver stateObserver)
        {
            observers.Add(stateObserver);
        }

        public void SetState(CombatStateType newState)
        {
            state = newState;

            foreach (ICombatStateObserver observer in observers)
            {
                observer.OnCombatStateChanged(state);
            }
        }
    }
}

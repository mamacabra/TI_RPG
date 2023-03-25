using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
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
            Debug.Log("Combat state changed to: " + state);

            foreach (ICombatStateObserver observer in observers)
            {
                observer.OnCombatStateChanged(state);
            }
        }
    }
}

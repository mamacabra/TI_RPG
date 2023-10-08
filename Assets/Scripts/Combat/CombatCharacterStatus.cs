using UnityEngine;

namespace Combat
{
    public class CombatCharacterStatus : MonoBehaviour, ICombatStateObserver
    {
        private void Start()
        {
            CombatState.Instance.Subscribe(this);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            switch (state)
            {
                case CombatStateType.HeroStatus:
                    StatusRules.MapStatus(CombatManager.Instance.HeroParty.Members);
                    CombatState.Instance.NextState();
                    break;
                case CombatStateType.EnemyStatus:
                    StatusRules.MapStatus(CombatManager.Instance.EnemyParty.Members);
                    CombatState.Instance.NextState();
                    break;
            }
        }
    }
}

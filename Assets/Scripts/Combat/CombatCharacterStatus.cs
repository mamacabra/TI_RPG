using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatCharacterStatus : MonoBehaviour, ICombatStateObserver
    {
        private const float StatusCoroutineDelay = 0.2f;

        private void Start()
        {
            CombatState.Instance.Subscribe(this);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            switch (state)
            {
                case CombatStateType.HeroStatus:
                    HeroMapStatus();
                    break;
                case CombatStateType.EnemyStatus:
                    EnemyMapStatus();
                    break;
            }
        }

        private void HeroMapStatus()
        {
            StartCoroutine(MapStatusCoroutine(CombatManager.Instance.HeroParty.Members));
        }

        private void EnemyMapStatus()
        {
            StartCoroutine(MapStatusCoroutine(CombatManager.Instance.EnemyParty.Members));
        }

        private static IEnumerator MapStatusCoroutine(List<Member> members)
        {
            foreach (var member in members)
            {
                if (member.Character.IsDead) continue;
                foreach (var status in member.Character.Status)
                {
                    DispatchStatusEffect(member.Character, status.type);
                    member.Character.Updated();
                    yield return new WaitForSeconds(StatusCoroutineDelay);
                };
            }

            yield return new WaitForSeconds(StatusCoroutineDelay);
            CombatState.Instance.NextState();
        }

        private static void DispatchStatusEffect(Character character, StatusType statusType)
        {
            switch (statusType)
            {
                case StatusType.Bleed:
                    StatusEffects.ApplyStatusBleed(character, statusType);
                    break;
                case StatusType.Stun:
                    StatusEffects.ApplyStatusStun(character, statusType);
                    break;
                case StatusType.Weak:
                    StatusEffects.ApplyStatusWeak(character, statusType);
                    break;
            }
        }
    }
}

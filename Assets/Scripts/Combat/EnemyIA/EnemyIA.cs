using System.Collections;
using UnityEngine;

namespace Combat
{
    public class EnemyIA : MonoBehaviour, ICombatStateObserver
    {
        private const float Delay = 0.6f;

        public void Start()
        {
            CombatState.Instance.Subscribe(this);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            if (state is not CombatStateType.EnemyTurn) return;
            StartCoroutine(nameof(AttackHeroesCoroutine));
        }

        private IEnumerator AttackHeroesCoroutine()
        {
            var enemies = CombatManager.Instance.EnemyParty.Members;

            if (enemies[0])
            {
                enemies[0].UseRandomCard(GetRandomHero());
                yield return new WaitForSeconds(Delay);
            }
            if (enemies[1])
            {
                enemies[1].UseRandomCard(GetRandomHero());
                yield return new WaitForSeconds(Delay);
            }
            if (enemies[2])
            {
                enemies[2].UseRandomCard(GetRandomHero());
                yield return new WaitForSeconds(Delay);
            }

            yield return new WaitForSeconds(Delay);
            CombatState.Instance.NextState();
        }

        private static Member GetRandomHero()
        {
            return CombatManager.Instance.HeroParty.GetRandomMember();
        }
    }
}

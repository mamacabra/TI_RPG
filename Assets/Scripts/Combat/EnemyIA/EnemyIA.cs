using System.Collections;
using UnityEngine;

namespace Combat
{
    public class EnemyIA : MonoBehaviour, ICombatStateObserver
    {
        private const float Delay = 0.4f;

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

            enemies[0].UseRandomCard(GetRandomHero());
            yield return new WaitForSeconds(Delay);
            enemies[1].UseRandomCard(GetRandomHero());
            yield return new WaitForSeconds(Delay);
            enemies[2].UseRandomCard(GetRandomHero());
            yield return new WaitForSeconds(Delay * 3);
            CombatState.Instance.NextState();
        }

        private static Member GetRandomHero()
        {
            return CombatManager.Instance.HeroParty.GetRandomMember();
        }
    }
}

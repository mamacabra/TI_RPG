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

            foreach (var enemy in enemies)
            {
                if (enemy && enemy.Character.IsDead == false)
                {
                    enemy.UseRandomCard(GetRandomHero());
                    yield return new WaitForSeconds(Delay);
                }
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

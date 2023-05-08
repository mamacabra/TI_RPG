using UnityEngine;

namespace Combat
{
    public class EnemyIA : MonoBehaviour, ICombatStateObserver
    {
        public void Start()
        {
            CombatState.Instance.Subscribe(this);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            if (state is not CombatStateType.EnemyTurn) return;
            AttackHeroes();
        }

        private static void AttackHeroes()
        {
            var enemies = CombatManager.Instance.EnemyParty.Members;

            enemies[0].UseRandomCard(GetRandomHero());
            enemies[1].UseRandomCard(GetRandomHero());
            enemies[2].UseRandomCard(GetRandomHero());

            CombatState.Instance.NextState();
        }

        // private static IEnumerator AttackHeroesCoroutine()
        // {
        //     if (CombatState.Instance.State is CombatStateType.EnemyTurn)
        //     {
        //         var enemies = CombatManager.Instance.Enemies;
        //
        //         CombatManager.UseRandomCard(enemies[0], GetRandomHero());
        //         yield return new WaitForSeconds(1.0f);
        //         CombatManager.UseRandomCard(enemies[1], GetRandomHero());
        //         yield return new WaitForSeconds(1.0f);
        //         CombatManager.UseRandomCard(enemies[2], GetRandomHero());
        //         yield return new WaitForSeconds(1.0f);
        //
        //         // CombatState.Instance.NextState();
        //     }
        // }

        private static Member GetRandomHero()
        {
            return CombatManager.Instance.HeroParty.GetRandomMember();
        }
    }
}

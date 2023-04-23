using System.Collections.Generic;
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
            var enemies = CombatManager.Instance.Enemies;
            CombatManager.UseRandomCard(enemies[0], GetRandomHero());
            CombatManager.UseRandomCard(enemies[1], GetRandomHero());
            CombatManager.UseRandomCard(enemies[2], GetRandomHero());
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
            List<Member> heroes = new List<Member>();
            foreach (var hero in CombatManager.Instance.Heroes)
            {
                if (hero.character.IsDead == false) heroes.Add(hero);
            }
            int r = Random.Range(0, heroes.Count);
            return heroes[r];
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class EnemyIA : MonoBehaviour, ICombatStateObserver
    {
        public void Start()
        {
            CombatState.Instance.AddObserver(this);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            if (state is CombatStateType.EnemyTurn) AttackHero();
        }

        private static void AttackHero()
        {
            foreach (var enemy in CombatManager.Instance.Enemies)
            {
                List<Character> targets = new List<Character>();
                foreach (var hero in CombatManager.Instance.Heroes)
                {
                    if (hero.character.isDead == false) targets.Add(hero.character);
                }
                int r = Random.Range(0, targets.Count);

                CombatManager.UseRandomCard(enemy, targets[r]);
            }

            CombatState.Instance.SetState(CombatStateType.EnemyDeckShuffle);
        }
    }
}

using System.Collections;
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

        private void AttackHero()
        {
            foreach (var enemy in CombatManager.Instance.Enemies)
            {
                List<Character> targets = new List<Character>();
                foreach (var hero in CombatManager.Instance.Heroes)
                {
                    if (hero.character.IsDead == false) targets.Add(hero.character);
                }
                int r = Random.Range(0, targets.Count);

                CombatManager.UseRandomCard(enemy, targets[r]);
            }

            StartCoroutine(nameof(WaitSeconds));
            CombatState.Instance.NextState();
        }

        private IEnumerator WaitSeconds()
        {
            yield return new WaitForSeconds(5.0f);
        }
    }
}

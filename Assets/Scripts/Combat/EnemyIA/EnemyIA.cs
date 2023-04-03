using System.Collections;
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

                StartCoroutine(WaitSeconds());
                CombatManager.UseRandomCard(enemy, targets[r]);
            }

            StartCoroutine(WaitSeconds(5.0f));
            CombatState.Instance.NextState();
        }

        private static IEnumerator WaitSeconds(float waitTime = 3.0f)
        {
                    Debug.Log("coroutineB created");
            yield return new WaitForSeconds(waitTime);
                    Debug.Log("coroutineB created");
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

namespace Combat
{
    public class CombatCharacterPassive : MonoBehaviour, ICombatStateObserver
    {
        private const float PassiveCoroutineDelay = 0.3f;

        private void Start()
        {
            CombatState.Instance.Subscribe(this);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            switch (state)
            {
                case CombatStateType.HeroPassive:
                    StartCoroutine(nameof(HeroBeforeTurnPassives));
                    break;
                case CombatStateType.EnemyPassive:
                    StartCoroutine(nameof(EnemyBeforeTurnPassives));
                    break;
            }
        }

        private IEnumerator HeroBeforeTurnPassives()
        {
            foreach (var member in CombatManager.Instance.HeroParty.Members)
            {
                if (member.Character.IsDead) continue;
                BeforeTurnPassives(member.Character);
                yield return new WaitForSeconds(PassiveCoroutineDelay);
            }

            yield return new WaitForSeconds(PassiveCoroutineDelay);
            CombatState.Instance.NextState();
        }

        private IEnumerator EnemyBeforeTurnPassives()
        {
            foreach (var member in CombatManager.Instance.EnemyParty.Members)
            {
                if (member.Character.IsDead) continue;
                BeforeTurnPassives(member.Character);
                yield return new WaitForSeconds(PassiveCoroutineDelay);
            };

            yield return new WaitForSeconds(PassiveCoroutineDelay);
            CombatState.Instance.NextState();
        }

        private static void BeforeTurnPassives(Character character)
        {
            switch (character.Passive)
            {
                case PassiveType.Elefante:
                    HeroElephantPassive.OnBeforeTurn(character);
                    break;
                case PassiveType.Tartaruga:
                    HeroTurtlePassive.OnBeforeTurn(character);
                    break;
            };
        }

        public static void Attack(Character character, Action action)
        {
            switch (character.Passive)
            {
                case PassiveType.Touro:
                    HeroBullPassive.OnAttack(character, action);
                    break;
                default:
                    action();
                    break;
            };
        }
    }
}

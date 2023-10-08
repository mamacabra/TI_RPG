using System;
using UnityEngine;

namespace Combat
{
    public class CombatCharacterPassive : MonoBehaviour, ICombatStateObserver
    {
        private void Start()
        {
            CombatState.Instance.Subscribe(this);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            switch (state)
            {
                case CombatStateType.HeroPassive:
                    HeroBeforeTurnPassives();
                    CombatState.Instance.NextState();
                    break;
                case CombatStateType.EnemyPassive:
                    EnemyBeforeTurnPassives();
                    CombatState.Instance.NextState();
                    break;
            }
        }

        private static void HeroBeforeTurnPassives()
        {
            CombatManager.Instance.HeroParty.Members.ForEach(member =>
            {
                if (member.Character.IsDead) return;
                BeforeTurnPassives(member.Character);
            });
        }

        private static void EnemyBeforeTurnPassives()
        {
            CombatManager.Instance.EnemyParty.Members.ForEach(member =>
            {
                if (member.Character.IsDead) return;
                BeforeTurnPassives(member.Character);
            });
        }

        private static void BeforeTurnPassives(Character character)
        {
            switch (character.Passive)
            {
                case PassiveType.Elephant:
                    HeroElephantPassive.OnBeforeTurn(character);
                    break;
                case PassiveType.Turtle:
                    HeroTurtlePassive.OnBeforeTurn(character);
                    break;
            };
        }

        public static void Attack(Character character, Action action)
        {
            switch (character.Passive)
            {
                case PassiveType.Bull:
                    HeroBullPassive.OnAttack(character, action);
                    break;
                default:
                    action();
                    break;
            };
        }
    }
}

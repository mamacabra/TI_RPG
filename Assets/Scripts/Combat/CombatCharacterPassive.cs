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
                BeforeTurnPassives(member.Character, member.Character.Passive);
            });
        }

        private static void EnemyBeforeTurnPassives()
        {
            CombatManager.Instance.EnemyParty.Members.ForEach(member =>
            {
                if (member.Character.IsDead) return;
                BeforeTurnPassives(member.Character, member.Character.Passive);
            });
        }

        private static void BeforeTurnPassives(Character character, PassiveType passive)
        {
            switch (passive)
            {
                case PassiveType.Elephant:
                    HeroElephantPassive.OnBeforeTurn(character);
                    break;
                case PassiveType.Turtle:
                    HeroTurtlePassive.OnBeforeTurn(character);
                    break;
            };
        }
    }
}

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
                    MapHeroPassives();
                    CombatState.Instance.NextState();
                    break;
                case CombatStateType.EnemyPassive:
                    ApplyEnemyPassive();
                    CombatState.Instance.NextState();
                    break;
            }
        }

        private static void MapHeroPassives()
        {
            CombatManager.Instance.HeroParty.Members.ForEach(member =>
            {
                if (member.Character.IsDead) return;
                ApplyHeroPassive(member.Character.HeroPassive);
            });
        }

        private static void ApplyHeroPassive(HeroPassiveType passive)
        {
            HeroBullPassive.OnBeforeTurn();
        }

        private static void ApplyEnemyPassive()
        {

        }
    }
}

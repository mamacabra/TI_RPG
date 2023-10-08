using System;

namespace Combat
{
    public abstract class HeroBullPassive : Passive
    {
        private const int SelfDoubleAttackChance = 100;

        public new static void OnAttack(Character character, Action action)
        {
            action();

            bool shouldDoubleAttack = CalculateChance(SelfDoubleAttackChance);
            if (shouldDoubleAttack)
            {
                action();
                AttackVFX.Instance.PlayHealingVFX(character.transform);
            }
        }
    }
}

namespace Combat
{
    public abstract class HeroElephantPassive : Passive
    {
        private const int SelfHealingChance = 100;
        private const int SelfHealingAmount = 2;
        private const int AllyHealingChance = 100;
        private const int AllyHealingAmount = 1;

        public new static void OnBeforeTurn(Character character)
        {
            bool shouldSelfHealing = CalculateChance(SelfHealingChance);
            if (shouldSelfHealing)
            {
                character.ReceiveHealing(SelfHealingAmount);
                AttackVFX.Instance.PlayHealingVFX(character.transform);
            }

            bool shouldAllyHealing = CalculateChance(AllyHealingChance);
            if (shouldAllyHealing)
            {
                Member ally = GetRandomAlly(character);
                if (ally != null)
                {
                    ally.Character.ReceiveHealing(AllyHealingAmount);
                    AttackVFX.Instance.PlayHealingVFX(character.transform);
                    AttackVFX.Instance.PlayHealingVFX(ally.Character.transform);
                }
            }
        }
    }
}

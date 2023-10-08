namespace Combat
{
    public abstract class HeroBullPassive : Passive
    {
        private const int SelfHealingChance = 20;
        private const int SelfHealingAmount = 2;
        private const int AllyHealingChance = 10;
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
                Member member = CombatManager.Instance.HeroParty.GetRandomMember();
                if (member != null) member.Character.ReceiveHealing(AllyHealingAmount);
            }
        }
    }
}

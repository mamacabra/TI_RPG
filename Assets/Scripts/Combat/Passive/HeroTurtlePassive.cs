namespace Combat
{
    public abstract class HeroTurtlePassive : Passive
    {
        private const int SelfShieldChance = 20;
        private const int SelfReflectChance = 20;

        public new static void OnBeforeTurn(Character character)
        {
            bool shouldSelfShield = CalculateChance(SelfShieldChance);
            if (shouldSelfShield)
            {
                character.ReceiveStatus(StatusType.Escudo);
                CombatLog.Instance.AddLog($"Passiva: A Tartaruga ganhou um escudo");
            }

            bool shouldSelfReflect = CalculateChance(SelfReflectChance);
            if (shouldSelfReflect)
            {
                character.ReceiveStatus(StatusType.Refletir);
                CombatLog.Instance.AddLog($"Passiva: A Tartaruga ganhou um escudo de reflexão");
            }
        }
    }
}

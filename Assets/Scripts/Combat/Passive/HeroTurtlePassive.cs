namespace Combat
{
    public abstract class HeroTurtlePassive : Passive
    {
        private const int SelfShieldChance = 100;
        private const int SelfReflectChance = 100;

        public new static void OnBeforeTurn(Character character)
        {
            bool shouldSelfShield = CalculateChance(SelfShieldChance);
            if (shouldSelfShield)
            {
                character.ReceiveStatus(StatusType.Shield);
            }

            bool shouldSelfReflect = CalculateChance(SelfReflectChance);
            if (shouldSelfReflect)
            {
                character.ReceiveStatus(StatusType.Reflect);
            }
        }
    }
}

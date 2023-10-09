namespace Combat
{
    public abstract class HeroTurtlePassive : Passive
    {
        private const int SelfShieldChance = 30;
        private const int SelfReflectChance = 5;

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

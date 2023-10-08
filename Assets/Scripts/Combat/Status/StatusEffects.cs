namespace Combat
{
    public static class StatusEffects
    {
        public static void ApplyStatusBleed(Character character, StatusType statusType)
        {
            character.ReceiveDamage();
            character.CountDownStatus(statusType);
            AttackVFX.Instance.PlayDropCardVFX(character.transform);
        }

        public static void ApplyStatusWeak(Character character, StatusType statusType)
        {
            character.ConsumeActionPoints();
            character.CountDownStatus(statusType);
            AttackVFX.Instance.PlayDropCardVFX(character.transform);
        }

        public static void ApplyStatusStun(Character character, StatusType statusType)
        {
            character.ClearActionPoints();
            character.CountDownStatus(statusType);
            AttackVFX.Instance.PlayDropCardVFX(character.transform);
        }
    }
}

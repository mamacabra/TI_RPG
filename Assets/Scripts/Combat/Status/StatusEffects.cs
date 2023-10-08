namespace Combat
{
    public static class StatusEffects
    {
        public static void ApplyStatusBleed(Character character)
        {
            character.ReceiveDamage();
            AttackVFX.Instance.PlayDamageVFX(character.transform);
        }
    }
}

namespace Combat
{
    public class Hero : Character, ICombatStateObserver
    {
        private void Start()
        {
            type = CharacterType.Hero;
            CombatState.Instance.AddObserver(this);
            CharacterCreated();
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            if (state is CombatStateType.HeroDeckShuffle) ResetActionPoints();
        }
    }
}

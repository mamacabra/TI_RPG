namespace Combat
{
    public class Hero : Character, ICombatStateObserver
    {
        public override CharacterType Type => CharacterType.Hero;

        private void Start()
        {
            CombatState.Instance.AddObserver(this);
            CharacterCreated();
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            if (state is CombatStateType.HeroDeckShuffle) ResetActionPoints();
        }
    }
}

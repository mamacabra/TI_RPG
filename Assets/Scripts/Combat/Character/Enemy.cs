namespace Combat
{
    public class Enemy : Character, ICombatStateObserver
    {
        public override CharacterType Type => CharacterType.Enemy;

        private void Start()
        {
            CombatState.Instance.AddObserver(this);
            CharacterCreated();
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            if (state is CombatStateType.EnemyDeckShuffle) ResetActionPoints();
        }
    }
}

namespace Combat
{
    public class Enemy : Character, ICombatStateObserver
    {
        private void Start()
        {
            type = CharacterType.Enemy;
            CombatState.Instance.AddObserver(this);
            CharacterCreated();
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            if (state is CombatStateType.EnemyDeckShuffle) ResetActionPoints();
        }
    }
}

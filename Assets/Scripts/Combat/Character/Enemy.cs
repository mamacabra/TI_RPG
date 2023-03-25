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
            switch (state)
            {
                case CombatStateType.HeroTurn:
                    ResetActionPoints();
                    break;
                case CombatStateType.EnemyTurn:
                    AttackHero();
                    break;
            }
        }

        private void AttackHero()
        {
            // Character[] characters = CombatManager.Instance.heroes;
            // UseRandomCard(characters);
        }
    }
}

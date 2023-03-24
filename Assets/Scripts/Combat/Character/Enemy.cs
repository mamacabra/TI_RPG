public class Enemy : Character, ICombatStateObserver
{
    CharacterType type = CharacterType.Hero;

    private void Start()
    {
        CombatState.Instance.AddObserver(this);
        CharacterCreated();
    }

    public void Notify(CombatStateType state)
    {
        switch (state)
        {
            case CombatStateType.PlayerTurn:
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

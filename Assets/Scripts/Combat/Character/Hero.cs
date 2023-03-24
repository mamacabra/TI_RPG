public class Hero : Character, ICombatStateObserver
{
    CharacterType type = CharacterType.Hero;

    private void Start()
    {
        CombatState.Instance.AddObserver(this);
        CharacterCreated();
    }

    public void Notify(CombatStateType state)
    {
        if (state is CombatStateType.EnemyTurn) ResetActionPoints();
    }
}

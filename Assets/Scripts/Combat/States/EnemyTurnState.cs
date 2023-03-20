public class EnemyTurnState : ICombatState
{
    public void Enter()
    {
        HudController.Instance.ShowEnemyHUD();

        foreach (var character in CombatManager.Instance.characters)
        {
            character.ResetActionPoints();
        }
    }
}

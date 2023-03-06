public class EnemyTurnState : ICombatState
{
    public void Enter()
    {
        HudController.Instance.ShowEnemyHUD();
    }
}

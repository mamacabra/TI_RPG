public class VictoryState : ICombatState
{
    public void Enter()
    {
        HudController.Instance.ShowVictoryModal();
    }
}

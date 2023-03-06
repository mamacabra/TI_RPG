public class DefeatState : ICombatState
{
    public void Enter()
    {
        HudController.Instance.ShowDefeatModal();
    }
}

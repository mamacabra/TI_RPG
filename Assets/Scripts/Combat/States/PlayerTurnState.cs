using UnityEngine;


public class PlayerTurnState : ICombatState
{
    private CombatFSM fsm;
    
    public void Enter()
    {
        HudController.Instance.ShowPlayerHUD();
    }
}

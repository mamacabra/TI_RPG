using UnityEngine;


public class PlayerTurnState : ICombatState
{
    private CombatState _state;
    
    public void Enter()
    {
        HudController.Instance.ShowPlayerHUD();
    }
}

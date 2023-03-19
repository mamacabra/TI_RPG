using System.Collections;
using UnityEngine;

public class CombatState : MonoBehaviour
{
    private ICombatState state;
    public static CombatState Instance;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        SetPlayerTurnState();
    }

    public void SetState(ICombatState newState)
    {
        state = newState;
        state?.Enter();
    }

    public void SetPlayerTurnState()
    {
        ICombatState newState = new PlayerTurnState();
        SetState(newState);
    }

    public void SetEnemyTurnState()
    {
        ICombatState newState = new EnemyTurnState();
        SetState(newState);

        StartCoroutine(nameof(EnemyTurnEnd)); // NOTE: temporary
    }

    private IEnumerator EnemyTurnEnd()
    {
        yield return new WaitForSeconds(2.0f);
        SetPlayerTurnState();
    }
}

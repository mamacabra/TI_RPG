using UnityEngine;
using UnityEngine.UI;

enum CharacterTurn
{
    Player,
    Enemy,
}

public class ButtonEndTurnHelper : MonoBehaviour
{
    [SerializeField] private CharacterTurn characterTurn = CharacterTurn.Player;

    public void Start()
    {
        AddClickListener();
    }

    private void AddClickListener()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(EndTurn);
    }

    private void EndTurn()
    {
        switch (characterTurn)
        {
            case CharacterTurn.Player:
                CombatState.Instance.SetState(CombatStateType.EnemyTurn);
                break;
            case CharacterTurn.Enemy:
                CombatState.Instance.SetState(CombatStateType.PlayerTurn);
                break;
        }
    }
}

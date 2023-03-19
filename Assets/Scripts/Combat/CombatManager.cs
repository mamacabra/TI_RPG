using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public Character[] characters;
    public Character[] enemies;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
    }

    public void CheckWinner()
    {
        int deadCharacters = characters.Count(player => player.isDead);
        int deadEnemies = enemies.Count(enemy => enemy.isDead);

        if (deadCharacters == characters.Length)
        {
            ICombatState newState = new DefeatState();
            CombatState.Instance.SetState(newState);
        }
        else if (deadEnemies == enemies.Length)
        {
            ICombatState newState = new VictoryState();
            CombatState.Instance.SetState(newState);
        }
    }
}

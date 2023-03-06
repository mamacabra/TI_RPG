using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{    
    public static CombatManager Instance;
    
    [SerializeField] private Character[] playerParty;
    [SerializeField] private Character[] enemyParty;
    
    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
    }

    public void CheckWinner()
    {
        int deadCharacters = playerParty.Count(player => player.isDead);
        int deadEnemies = enemyParty.Count(enemy => enemy.isDead);
        
        if (deadCharacters == playerParty.Length)
        {
            ICombatState newState = new DefeatState();
            CombatFSM.Instance.SetState(newState);
        }
        else if (deadEnemies == enemyParty.Length)
        {
            ICombatState newState = new VictoryState();
            CombatFSM.Instance.SetState(newState);
        }
    }
}

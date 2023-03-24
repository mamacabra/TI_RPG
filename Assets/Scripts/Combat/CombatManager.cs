using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour, ICharacterObserver
{
    public static CombatManager Instance;

    public List<Character> heroes;
    public List<Character> enemies;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CombatState.Instance.SetState(CombatStateType.Start);
        CombatState.Instance.SetState(CombatStateType.PlayerTurn);
    }

    public void CharacterCreated(Character character)
    {
        if (character.type == CharacterType.Hero) heroes.Add(character);
        else enemies.Add(character);
    }

    public void CharacterUpdated(Character character)
    {
        if (character.type == CharacterType.Hero) CheckHeroesDead();
        else CheckEnemiesDead();
    }

    private void CheckHeroesDead()
    {
        int deadHeroes = heroes.Count(hero => hero.isDead);
        if (deadHeroes == heroes.Count)
        {
            CombatState.Instance.SetState(CombatStateType.Defeat);
        }
    }

    private void CheckEnemiesDead()
    {
        int deadEnemies = enemies.Count(enemy => enemy.isDead);
        if (deadEnemies == enemies.Count)
        {
            CombatState.Instance.SetState(CombatStateType.Victory);
        }
    }
}

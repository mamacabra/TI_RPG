public class Enemy
{
    public static void AttackCharacter(Character enemy)
    {
        Character[] characters = CombatManager.Instance.characters;
        enemy.UseRandomCard(characters);
    }
}

using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Hero,
    Enemy,
}

public abstract class Character : MonoBehaviour
{
    [SerializeField] public CharacterType type;

    [Header("Observers")]
    [SerializeField] private HealthBar healthBar;

    public bool isDead => health <= 0;

    [Header("Information")]
    public int health;
    public int maxHealth = 10;
    public int actionPoints;
    public int maxActionPoints = 3;

    [Header("Cards")]
    public List<Card> hand = new List<Card>();
    public Deck deck;

    protected void CharacterCreated()
    {
        health = maxHealth;
        actionPoints = maxActionPoints;
        healthBar.CharacterCreated(this);
        CombatManager.Instance.CharacterCreated(this);
        SetupDeck();
    }

    private void CharacterUpdated()
    {
        healthBar.CharacterUpdated(this);
        CombatManager.Instance.CharacterUpdated(this);
    }

    public void ReceiveDamage(int value = 1)
    {
        health -= value;
        if (health < 0) health = 0;

        CharacterUpdated();
    }

    public void ReceiveHealing(int value = 1)
    {
        health += value;
        if (health > maxHealth) health = maxHealth;

        CharacterUpdated();
    }

    public void ConsumeActionPoints(int value = 1)
    {
        actionPoints -= value;
        if (actionPoints < 0) actionPoints = 0;

        CharacterUpdated();
    }

    public void ResetActionPoints()
    {
        actionPoints = maxActionPoints;
        CharacterUpdated();
    }

    public void UseRandomCard(List<Character> allTargets)
    {
        int r = Random.Range(0, hand.Count);
        UseCard(hand[r], allTargets);
    }

    public void UseCard(Card card, List<Character> allTargets)
    {
        List<Character> targets = new List<Character>();
        foreach (var target in allTargets)
        {
            if (target.isDead == false) targets.Add(target);
        }
        int r = Random.Range(0, targets.Count);

        if (actionPoints >= card.Cost)
        {
            actionPoints -= card.Cost;
            CharacterUpdated();

            if (card.Damage > 0)
            {
                targets[r].ReceiveDamage(card.Damage);
            }
            if (card.Heal > 0)  ReceiveHealing(card.Heal);
        }
    }

    private void SetupDeck()
    {
        deck = new Deck();

        deck.AddCard(new Card()
        {
            Name = "Curar",
            Cost = 2,
            Heal = 2,
        });
        deck.AddCard(new Card()
        {
            Name = "Ataque Fraco",
            Cost = 1,
            Damage = 1,
        });
        deck.AddCard(new Card()
        {
            Name = "Ataque Fraco 2",
            Cost = 1,
            Damage = 2,
        });
        deck.AddCard(new Card()
        {
            Name = "Ataque Médio",
            Cost = 2,
            Damage = 2,
        });
        deck.AddCard(new Card()
        {
            Name = "Ataque Médio 2",
            Cost = 2,
            Damage = 3,
        });
        deck.AddCard(new Card()
        {
            Name = "Ataque FORTE",
            Cost = 3,
            Damage = 5,
        });

        hand = deck.Shuffle();
    }
}

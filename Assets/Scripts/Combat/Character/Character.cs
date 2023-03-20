
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public bool isDead => health <= 0;

    [Header("Character Information")]
    public int health;
    public int maxHealth = 10;
    public int actionPoints;
    public int maxActionPoints = 3;

    [Header("Health Bar Objects")]
    public Slider healthBarSlider;
    public Text healthBarCount;
    public Text actionPointsCount;

    [Header("Cards")]
    public List<Card> hand;
    public Deck deck;

    private void Awake()
    {
        SetupDeck();
    }

    private void Start()
    {
        SetupHealthBar();
    }

    public void UseCard(Card card)
    {
        List<Character> enemies = new List<Character>();
        foreach (var enemy in CombatManager.Instance.enemies)
        {
            if (enemy.isDead == false) enemies.Add(enemy);
        }
        int r = Random.Range(0, enemies.Count);

        if (actionPoints >= card.Cost)
        {
            actionPoints -= card.Cost;
            UpdateHealthBar();

            if (card.Damage > 0)
            {
                enemies[r].Damage(card.Damage);
            }
            if (card.Heal > 0)  Heal(card.Heal);
        }
    }

    public void Damage(int damage = 1)
    {
        health -= damage;
        if (health < 0) health = 0;

        UpdateHealthBar();
        if (health == 0) CombatManager.Instance.CheckWinner();
    }

    public void Heal(int value = 1)
    {
        health += value;
        if (health > maxHealth) health = maxHealth;

        UpdateHealthBar();
    }

    private void SetupHealthBar()
    {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
        actionPoints = maxActionPoints;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBarSlider.value = health;
        healthBarCount.text = "HP: " + health;
        actionPointsCount.text = "AP: " + actionPoints;
    }

    private void SetupDeck()
    {
        deck = new Deck();

        deck.AddCard(new Card()
        {
            Name = "Ataque Fraco",
            Cost = 1,
            Damage = 1,
        });
        deck.AddCard(new Card()
        {
            Name = "Curar",
            Cost = 2,
            Heal = 2,
        });
        deck.AddCard(new Card()
        {
            Name = "Ataque Médio",
            Cost = 2,
            Damage = 3,
        });
        deck.AddCard(new Card()
        {
            Name = "Curar Muito",
            Cost = 3,
            Heal = 4,
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

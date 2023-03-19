using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public bool isDead => health <= 0;

    [Header("Character Information")]
    public int health = 5;
    public int maxHealth = 5;

    [Header("Health Bar Objects")]
    public Slider healthBarSlider;
    public Text healthBarCount;

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
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBarSlider.value = health;
        healthBarCount.text = health.ToString();
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
            Damage = 4,
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

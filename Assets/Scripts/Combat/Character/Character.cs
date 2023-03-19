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
}

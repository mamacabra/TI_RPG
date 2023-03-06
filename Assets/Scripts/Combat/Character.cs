using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header("Character Information")]
    public int health = 5;
    public int totalHealth = 5;

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
    }

    public void Heal(int value = 1)
    {
        health += value;
        if (health > totalHealth) health = totalHealth;

        UpdateHealthBar();
    }

    private void SetupHealthBar()
    {
        healthBarSlider.maxValue = totalHealth;
        healthBarSlider.value = totalHealth;
        UpdateHealthBar();
    } 

    private void UpdateHealthBar()
    {
        healthBarSlider.value = health;
        healthBarCount.text = health.ToString();
    }
}

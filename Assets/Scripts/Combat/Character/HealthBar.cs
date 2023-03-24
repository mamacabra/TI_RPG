﻿using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, ICharacterObserver
{
    [Header("Slider")]
    public Slider healthBarSlider;

    [Header("Counters")]
    public Text healthBarCount;
    public Text actionPointsCount;

    public void OnCharacterCreated(Character character)
    {
        healthBarSlider.maxValue = character.maxHealth;
        OnCharacterUpdated(character);
    }

    public void OnCharacterUpdated(Character character)
    {
        healthBarSlider.value = character.health;
        healthBarCount.text = "HP: " + character.health;
        actionPointsCount.text = "AP: " + character.actionPoints;
    }
}
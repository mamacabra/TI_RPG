using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;

[System.Serializable]
public struct CharacterSave
{
    public bool isDead;
}
public class SaveDeath : MonoBehaviour
{
    private static SaveDeath instance;
    public static SaveDeath Instance => instance ? instance : FindObjectOfType<SaveDeath>();
    
    public CharacterSave[] CharacterSaveData = new CharacterSave[3];

    public void Start()
    {
        for (int i = 0; i < CharacterSaveData.Length; i++)
        {
            CharacterSaveData[i].isDead = false;
        }
    }

    public void ChangeStatus(Character character)
    {
        CharacterSaveData[character.CharacterIndex].isDead = character.IsDead;
    }

    public bool LoadStatus(Character character)
    {
       
        return CharacterSaveData[character.CharacterIndex].isDead;
        
    }

    public void CheckGameOver()
    {
        int count = 0;
        for (int i = 0; i < CharacterSaveData.Length; i++)
        {
            if(CharacterSaveData[i].isDead) count++;
        }

        if (count >= 3)
        {
            MapManager.Instance.GameOver();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;


/*[System.Serializable]
public struct CharacterSave
{
    //public int characterIndex;
    public bool isDead;
}*/

[System.Serializable]
public class DeathSaveData : MonoBehaviour
{
    //public CharacterSave[] CharacterSaveData = new CharacterSave[3];

    public DeathSaveData(Character character)
    {
        //CharacterSaveData[index].characterIndex = character.CharacterIndex;
        //CharacterSaveData[character.CharacterIndex].isDead = character.IsDead;
    }

}

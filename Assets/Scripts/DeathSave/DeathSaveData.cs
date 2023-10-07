using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;

[System.Serializable]
public class DeathSaveData : MonoBehaviour
{
    
        private bool isDead;
        public bool IsDead
        {
            get => IsDead;
            set { IsDead = value; }
        }
        
        public DeathSaveData(Character character)
        {
          
            //IsDead = character.IsDead;
        }

}

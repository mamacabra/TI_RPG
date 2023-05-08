using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Card", menuName = "Card")]
    public class CardScriptableObject : ScriptableObject
    {
        [Space]
        public string label;
        [TextArea] public string description;

        [Header("Action Points")]
        [Range(-3, 3)] public int cost;
        [Range(1, 10)] public int receive;

        [Header("Stats")]
        [Range(1, 10)] public int damage;
        [Range(1, 10)] public int heal;

        [Header("Effects")]
        [Range(1, 3)] public int drawCard;
        // [Range(1, 3)] public int drawPartyCard;

        [Header("Target Effects")]
        public List<CardScriptableObject> addCardOnTargetDeck;
        [Range(1, 3)] public int dropCardOnTargetHand;
    }
}
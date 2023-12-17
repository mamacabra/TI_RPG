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

        [Space]
        public Sprite sprite;

        [Header("Action Points")]
        [Range(-3, 3)] public int cost;
        [Range(0, 10)] public int receive;

        [Header("Stats")]
        [Range(0, 20)] public int damage;
        [Range(0, 10)] public int heal;
        [Range(0, 10)] public int selfHeal;

        [Header("Effects")]
        [Range(0, 3)] public int drawCard;
        // [Range(1, 3)] public int drawPartyCard;

        [Header("Target Effects")]
        public List<CardScriptableObject> addCardOnTargetDeck;
        [Range(0, 3)] public int dropCardOnTargetHand;

        [Header("Status")]
        public bool statusBewitch;
        public bool statusBleed;
        public bool statusBurn;
        public bool statusConfuse;
        public bool statusCurse;
        public bool statusFreeze;
        public bool statusPierce;
        public bool statusPoison;
        public bool statusReflect;
        public bool statusStun;
        public bool statusUnlucky;
        public bool statusVulnerable;
        public bool statusWeak;
    }
}

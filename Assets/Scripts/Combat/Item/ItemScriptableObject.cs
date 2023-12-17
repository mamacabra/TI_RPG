using UnityEngine;

namespace Combat
{
    public enum E_ItemRarity{
        COMMOM,
        RARE,
        EPIC,
        LENGENDARY,
        NONE,
    }

    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class ItemScriptableObject : ScriptableObject
    {
        public string cardName;
        [TextArea] public string description;
        public string resourcePath;
        public E_ItemRarity rarity;
        [Space]
        public Sprite sprite;

        public CardScriptableObject[] cards = new CardScriptableObject[3];
    }
}

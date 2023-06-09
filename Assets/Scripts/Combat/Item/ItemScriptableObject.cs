using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class ItemScriptableObject : ScriptableObject
    {
        public string name;
        [TextArea] public string description;

        [Space]
        public Sprite sprite;

        public CardScriptableObject[] cards = new CardScriptableObject[3];
    }
}

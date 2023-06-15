using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class ItemScriptableObject : ScriptableObject
    {
        public string name;
        [TextArea] public string description;
        public string resourcePath;

        [Space]
        public Sprite sprite;

        public CardScriptableObject[] cards = new CardScriptableObject[3];
    }
}

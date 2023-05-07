using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "Card", menuName = "Card")]
    public class CardScriptableObject : ScriptableObject
    {
        public string name;
        public int cost;
    }
}

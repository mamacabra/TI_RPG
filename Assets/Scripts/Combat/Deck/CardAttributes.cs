using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class CardAttributes : MonoBehaviour
    {
        public Text title;
        public Text cost;
        public Text damage;
        public Text heal;

        public void Setup(Card card)
        {
            title.text = card.Name;
            cost.text = "Cost: " + card.Cost;
            damage.text = "Damage: " + card.Damage;
            heal.text = "Heal: " + card.Heal;
        }
    }
}

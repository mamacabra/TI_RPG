using UnityEngine;

namespace Combat
{
    public class PlayerCards : MonoBehaviour
    {
        public GameObject cardPrefab;

        private void Start()
        {
            int cardIndex = 0;
            foreach (var hero in CombatManager.Instance.Heroes)
            {
                foreach (var card in hero.hand)
                {
                    GameObject gb = Instantiate(cardPrefab, transform);
                    CardBase cardBase = gb.GetComponent<CardBase>();
                    cardBase.Setup(hero.character, card, cardIndex);
                    cardIndex++;
                }
            }
        }
    }
}

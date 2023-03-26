using UnityEngine;

namespace Combat
{
    public class PlayerCards : MonoBehaviour
    {
        public GameObject cardPrefab;

        private void OnEnable()
        {
            DestroyCards();
            CreateCards();
        }

        private void CreateCards()
        {
            int i = 0;
            foreach (var hero in CombatManager.Instance.Heroes)
            {
                foreach (var card in hero.hand)
                {
                    GameObject gameObject = Instantiate(cardPrefab, transform);
                    CardBase cardBase = gameObject.GetComponent<CardBase>();
                    cardBase.Setup(hero.character, card, i);
                    i++;
                }
            }
        }

        private void DestroyCards()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

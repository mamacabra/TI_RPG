using UnityEngine;

namespace Combat
{
    public class PlayerCards : MonoBehaviour
    {
        public GameObject cardPrefab;
        public static PlayerCards Instance { get; private set; }

        private void Start()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            DrawCards();
        }

        public void DrawCards()
        {
            DestroyAllCards();
            CreateCards();
        }

        private void CreateCards()
        {
            int i = 0;
            foreach (Member member in CombatManager.Instance.HeroParty.Members)
            {
                foreach (Card card in member.Hand)
                {
                    GameObject cardGameObject = Instantiate(cardPrefab, transform);
                    Card3D c = cardGameObject.GetComponent<Card3D>();
                    c.Setup(member, card, i);
                    i++;
                }
            }
        }

        private void DestroyAllCards()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}

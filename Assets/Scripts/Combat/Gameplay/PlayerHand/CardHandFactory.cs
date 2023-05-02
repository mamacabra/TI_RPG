using System;
using UnityEngine;

namespace Combat
{
    public class CardHandFactory : MonoBehaviour
    {
        public GameObject cardPrefab;
        public static CardHandFactory Instance { get; private set; }

        private void Start()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            DrawCards();
        }

        private void OnDisable()
        {
            DestroyAllCards();
        }

        public void DrawCards()
        {
            DestroyAllCards();
            InstantiateHandCards();
        }

        private void InstantiateHandCards()
        {
            int i = 0;
            foreach (Member member in CombatManager.Instance.HeroParty.Members)
            {
                foreach (Card card in member.Hand)
                {
                    GameObject cardGameObject = Instantiate(cardPrefab, transform);

                    CardController cardController = cardGameObject.GetComponent<CardController>();
                    cardController.Setup(member, card, i);

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

using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class HandController : MonoBehaviour
    {
        public GameObject cardPrefab;
        public static HandController Instance { get; private set; }

        [SerializeField] private List<CardController> cards;

        private void Start()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            cards = new List<CardController>();
            InstantiateCards();
            SetupCardsPosition();
        }

        private void OnDisable()
        {
            cards = new List<CardController>();
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        private void InstantiateCards()
        {
            if (CombatManager.Instance.HeroParty == null || CombatManager.Instance.HeroParty.Members == null) return;

            foreach (Member member in CombatManager.Instance.HeroParty.Members)
            {
                if (member == null || member.Hand == null || member.Character.IsDead || member.Character.HasActionPoints == false) continue;

                foreach (Card card in member.Hand)
                {
                    if (card.ActionPointsCost > member.Character.ActionPoints) continue;
                    InstantiateCard(member, card);
                }
            }
        }

        private void InstantiateCard(Member member, Card card)
        {
            GameObject cardGameObject = Instantiate(cardPrefab, transform);

            CardController cardController = cardGameObject.GetComponent<CardController>();
            cardController.Setup(member, card);

            cards.Add(cardController);
        }

        public void AddCard(Member member, Card card)
        {
            InstantiateCard(member, card);
            SetupCardsPosition();
        }

        private void SetupCardsPosition()
        {
            if (cards.Count <= 0) return;

            int middleCard = (cards.Count -1) / 2;

            for (int i = middleCard - 1, p = -1; i >= 0; i--, p--)
                cards[i].Display.Setup(p);

            cards[middleCard].Display.Setup();

            for (int i = middleCard + 1, p = 1; i < cards.Count; i++, p++)
                cards[i].Display.Setup(p);
        }

        public void RemoveUsedCard(CardController card)
        {
            if (card == null) return;

            cards.Remove(card);
            Destroy(card.gameObject);
            SetupCardsPosition();
        }

        public void RemoveUnavailableCards()
        {
            List<CardController> availableCards = new List<CardController>();

            foreach (CardController card in cards)
            {
                if (card.IsAvailable) availableCards.Add(card);
                else Destroy(card.gameObject);
            }

            cards = availableCards;
        }
    }
}

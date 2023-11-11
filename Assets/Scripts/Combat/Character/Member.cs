using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Member : MonoBehaviour
    {
        private Deck Deck { get; set; }

        public Character Character { get; set; }
        public List<Card> Hand { get; private set; }

        public void AddCard(CardScriptableObject cardData)
        {
            if (cardData == null) return;
            Deck.AddCard(new Card(cardData));
        }

        public void SetupDeck(List<CardScriptableObject> cards)
        {
            Deck = new Deck();
            foreach (var card in cards) AddCard(card);
            ShuffleDeck();
        }

        public void ShuffleDeck()
        {
            Hand = Deck.Shuffle();
        }

        public Card? DrawHandCard()
        {
            var card = Deck.DrawCard(Hand);

            if (card == null) return null;

            Hand.Add((Card) card);
            return card;
        }

        public void DropHandCard()
        {
            int r = Random.Range(0, Hand.Count);
            Card card = Hand[r];
            Hand.Remove(card);
        }

        public Card GetRandomCard()
        {
            int r = Random.Range(0, Hand.Count);
            Card card = Hand[r];
            return card;
        }

        public void UseRandomCard(Member target, Card card)
        {
            if (Character.HasEnoughActionPoints(card.ActionPointsCost) == false || target == null) return;

            CardBehavior.Use(this, card, target);
            Hand.Remove(card);
        }
    }
}

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

        public Card DrawHandCard()
        {
            Card card = Deck.DrawCard(Hand);
            Hand.Add(card);
            return card;
        }

        public void DropHandCard()
        {
            int r = Random.Range(0, Hand.Count);
            Card card = Hand[r];
            Hand.Remove(card);
        }

        public void UseRandomCard(Member target)
        {
            int r = Random.Range(0, Hand.Count);
            Card card = Hand[r];

            if (Character.HasEnoughActionPoints(card.Cost))
            {
                CardBehavior.Use(this, card, target);
                Hand.Remove(card);
            }
        }
    }
}

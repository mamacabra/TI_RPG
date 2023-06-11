using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Deck
    {
        private List<Card> Cards { get; }

        public Deck()
        {
            Cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public List<Card> Shuffle(int count = 3)
        {
            List<Card> possibleCards = new List<Card>(Cards);
            List<Card> shuffledCards = new List<Card>();

            for (int i = 0; i < count; i++)
            {
                if (possibleCards.Count == 0) break;

                int r = Random.Range(0, possibleCards.Count);
                shuffledCards.Add(possibleCards[r]);
                possibleCards.RemoveAt(r);
            }

            return shuffledCards;
        }

        public Card? DrawCard(List<Card> hand)
        {
            List<Card> possibleCards = new List<Card>(Cards);
            foreach (Card card in hand) possibleCards.Remove(card);

            if (possibleCards.Count == 0) return null;

            int r = Random.Range(0, possibleCards.Count);
            return possibleCards[r];
        }
    }
}

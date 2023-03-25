using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Deck
    {
        public List<Card> Cards { get; set; }

        public Deck()
        {
            Cards = new List<Card>();
        }

        public Deck(List<Card> cards)
        {
            Cards = cards;
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }

        public List<Card> Shuffle(int count = 3)
        {
            List<Card> possibleCards = new List<Card>(Cards);
            List<Card> shuffledCards = new List<Card>();

            for (int i = 0; i < count; i++)
            {
                int randomIndex = Random.Range(0, possibleCards.Count);
                shuffledCards.Add(possibleCards[randomIndex]);
                possibleCards.RemoveAt(randomIndex);
            }

            return shuffledCards;
        }
    }
}

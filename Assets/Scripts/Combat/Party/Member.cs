using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Member
    {
        public Character character;
        public Deck deck;
        public List<Card> hand;

        public Member(Character character, Deck deck)
        {
            this.character = character;
            this.deck = deck;
            hand = this.deck.Shuffle();
        }

        public void DrawRandomCard(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                hand.Add(deck.DrawCard(hand));
            }
        }

        public void DropRandomCard(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                if (hand.Count == 0) break;

                int r = Random.Range(0, hand.Count);
                hand.RemoveAt(r);
            }
        }

        public void AddEmptyCard()
        {
            Debug.Log(deck.CardsCount);
            deck.AddCard(new Card()
            {
                Name = "KKKKK",
                Cost = 1,
            });
            Debug.Log(deck.CardsCount);
        }
    }
}

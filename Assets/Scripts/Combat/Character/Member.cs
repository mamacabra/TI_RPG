using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Member : MonoBehaviour
    {
        public Character Character { get; set; }
        private Deck Deck { get; set; }
        public List<Card> Hand;

        public void AddCard(CardScriptableObject card)
        {
            if (card == null) return;

            Deck.AddCard(new Card()
            {
                Label = card.label,
                Description = card.description,
                Cost = card.cost,
                ActionPointsReceive = card.receive,
                Damage = card.damage,
                Heal = card.heal,
                DrawCard = card.drawCard,
                DropTargetCard = card.dropCardOnTargetHand,
                AddCards = card.addCardOnTargetDeck,
            });
        }

        public void SetupDeck(List<CardScriptableObject> cards)
        {
            Deck = new Deck();

            foreach (CardScriptableObject card in cards)
                AddCard(card);

            Hand = Deck.Shuffle();
        }

        public void ShuffleDeck()
        {
            Hand = Deck.Shuffle();
        }

        public Card DrawCard()
        {
            Card card = Deck.DrawCard(Hand);
            Hand.Add(card);
            return card;
        }

        public void DropHandCard(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                if (Hand.Count == 0) break;

                int r = Random.Range(0, Hand.Count);
                Hand.RemoveAt(r);
            }
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

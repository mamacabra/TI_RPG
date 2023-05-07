using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Member : MonoBehaviour
    {
        public Character Character { get; set; }
        public Deck Deck { get; private set; }
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

        private void AddCards(List<CardScriptableObject> cards)
        {
            foreach (CardScriptableObject card in cards) AddCard(card);
        }

        public void SetupDeck(List<CardScriptableObject> cards)
        {
            Deck = new Deck();
            AddCards(cards);
            Hand = Deck.Shuffle();
        }

        public void DrawCard(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
                Hand.Add(Deck.DrawCard(Hand));
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

        public void UseCard(Card card, Member target)
        {
            if (Character.HasEnoughActionPoints(card.Cost) == false) return;

            CardBehavior.Use(this, card, target);
            Hand.Remove(card);
            HandFactory.Instance.CreateCards();
        }

        public void UseRandomCard(Member target)
        {
            int r = Random.Range(0, Hand.Count);
            UseCard(Hand[r], target);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(Character))]
    public class Member : MonoBehaviour
    {
        public Character Character { get; private set; }
        public Deck Deck { get; private set; }
        public List<Card> Hand;

        private void Awake()
        {
            Character = GetComponent<Character>();
            Deck = DeckFactory.CreateDeck(Character.Type);
            Hand = Deck.Shuffle();
        }

        private void DrawRandomCard(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                Hand.Add(Deck.DrawCard(Hand));
            }
        }

        private void DropRandomCard(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                if (Hand.Count == 0) break;

                int r = Random.Range(0, Hand.Count);
                Hand.RemoveAt(r);
            }
        }

        private void AddEmptyCard()
        {
            Debug.Log(Deck.CardsCount);
            Deck.AddCard(new Card()
            {
                Name = "KKKKK",
                Cost = 1,
            });
            Debug.Log(Deck.CardsCount);
        }

        public void UseCard(Card card, Member target)
        {
            if (Character.ConsumeActionPoints(card.Cost) == false) return;

            if (card.Damage > 0)
            {
                target.Character.ReceiveDamage(card.Damage);
                VFXManager.Instance.PlayDamageVFX(target.Character.transform);
            }
            if (card.Heal > 0)
            {
                target.Character.ReceiveHealing(card.Heal);
                VFXManager.Instance.PlayHealingVFX(target.Character.transform);
            };
            if (card.DrawCard > 0)
            {
                DrawRandomCard(card.DrawCard);
            }
            if (card.DropTargetCard > 0)
            {
                target.DropRandomCard(card.DropTargetCard);
            }
            if (card.AddEmptyCard)
            {
                target.AddEmptyCard();
            }

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

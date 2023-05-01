using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class Member
    {
        public readonly Character Character;
        public readonly Deck Deck;
        public List<Card> Hand;

        public Member(Character character, Deck deck)
        {
            this.Character = character;
            this.Deck = deck;
            this.Hand = deck.Shuffle();
        }

        public void DrawRandomCard(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                Hand.Add(Deck.DrawCard(Hand));
            }
        }

        public void DropRandomCard(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                if (Hand.Count == 0) break;

                int r = Random.Range(0, Hand.Count);
                Hand.RemoveAt(r);
            }
        }

        public void AddEmptyCard()
        {
            Debug.Log(Deck.CardsCount);
            Deck.AddCard(new Card()
            {
                Name = "KKKKK",
                Cost = 1,
            });
            Debug.Log(Deck.CardsCount);
        }

        public static void UseCard(Member member, Card3D card, Member target)
        {
            if (member.Character.ConsumeActionPoints(card.card.Cost) == false) return;

            if (card.card.Damage > 0)
            {
                target.Character.ReceiveDamage(card.card.Damage);
                VFXManager.Instance.PlayDamageVFX(target.Character.transform);
            }
            if (card.card.Heal > 0)
            {
                target.Character.ReceiveHealing(card.card.Heal);
                VFXManager.Instance.PlayHealingVFX(target.Character.transform);
            };
            if (card.card.DrawCard > 0)
            {
                member.DrawRandomCard(card.card.DrawCard);
            }
            if (card.card.DropTargetCard > 0)
            {
                target.DropRandomCard(card.card.DropTargetCard);
            }
            if (card.card.AddEmptyCard)
            {
                target.AddEmptyCard();
                Debug.Log("AddEmptyCard");
            }

            member.Hand.Remove(card.card);
            PlayerCards.Instance.DrawCards();
        }

        public static void UseCard(Member member, Card card, Member target)
        {
            if (member.Character.ConsumeActionPoints(card.Cost) == false) return;

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
                member.DrawRandomCard(card.DrawCard);
                Debug.Log("DrawCard");
            }
            if (card.DropTargetCard > 0)
            {
                target.DropRandomCard(card.DropTargetCard);
                Debug.Log("DropTargetCard");
            }
            if (card.AddEmptyCard)
            {
                target.AddEmptyCard();
                Debug.Log("AddEmptyCard");
            }

            member.Hand.Remove(card);
            PlayerCards.Instance.DrawCards();
        }

        public void UseRandomCard(Member target)
        {
            int r = Random.Range(0, Hand.Count);
            UseCard(this, Hand[r], target);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Combat
{
    public class Party
    {
        public bool IsDefeated => Members.TrueForAll(member => member.Character.IsDead);
        public List<Member> Members { get; private set; }

        public Party(List<Character> characters)
        {
            Members = new List<Member>();
            foreach (Character character in characters)
            {
                Deck deck = DeckFactory.CreateDeck(character.Type);
                Member member = new Member(character, deck);
                Members.Add(member);
            }
        }

        public void ShuffleDeck()
        {
            foreach (Member member in Members)
            {
                member.Hand = member.Deck.Shuffle();
            }
        }

        public void ResetActionPoints()
        {
            foreach (Member member in Members)
            {
                member.Character.ResetActionPoints();
            }
        }

        public Member GetRandomMember()
        {
            List<Member> livingMembers = Members.Where(member => member.Character.IsDead == false).ToList();
            int r = Random.Range(0, livingMembers.Count);
            return livingMembers[r];
        }
    }
}

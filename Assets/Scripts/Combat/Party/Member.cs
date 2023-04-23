using System.Collections.Generic;

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

        public void DrawHeroCards(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                hand.Add(deck.DrawCard(hand));
            }
        }
    }
}

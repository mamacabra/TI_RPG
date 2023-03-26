using System.Collections.Generic;

namespace Combat
{
    public class CharacterData
    {
        public Character character;
        public Deck deck;
        public List<Card> hand;

        public CharacterData(Character character, Deck deck)
        {
            this.character = character;
            this.deck = deck;
            hand = this.deck.Shuffle();
        }
    }
}

using System.Collections.Generic;

namespace Combat
{
    public class Party
    {
        public List<CharacterData> Members { get; private set; }

        private void ShuffleDeck()
        {
            foreach (CharacterData character in Members)
            {
                character.hand = character.deck.Shuffle();
            }
        }

        private void ResetActionPoints()
        {
            foreach (CharacterData character in Members)
            {
                character.character.ResetActionPoints();
            }
        }
    }
}

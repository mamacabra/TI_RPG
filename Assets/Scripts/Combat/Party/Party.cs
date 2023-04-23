using System.Collections.Generic;

namespace Combat
{
    public class Party
    {
        public List<Member> Members { get; private set; }

        private void ShuffleDeck()
        {
            foreach (Member character in Members)
            {
                character.hand = character.deck.Shuffle();
            }
        }

        private void ResetActionPoints()
        {
            foreach (Member character in Members)
            {
                character.character.ResetActionPoints();
            }
        }
    }
}

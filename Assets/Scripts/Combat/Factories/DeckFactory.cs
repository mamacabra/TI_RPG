using System.Collections.Generic;

namespace Combat
{
    public class DeckFactory
    {
        public DeckFactory(List<Character> heroes, List<Character> enemies)
        {
            CreateCharacterDeck(heroes);
            CreateCharacterDeck(enemies);
        }

        private static void CreateCharacterDeck(List<Character> characters)
        {
            foreach (var character in characters)
            {
                Deck deck = CreateDeck();
                CombatManager.Instance.SetCharacterDeck(character, deck);
            }
        }

        private static Deck CreateDeck()
        {
            Deck deck = new Deck();

            deck.AddCard(new Card()
            {
                Name = "Curar",
                Cost = 2,
                Heal = 2,
            });
            deck.AddCard(new Card()
            {
                Name = "Ataque Fraco",
                Cost = 1,
                Damage = 1,
            });
            deck.AddCard(new Card()
            {
                Name = "Ataque Fraco 2",
                Cost = 1,
                Damage = 2,
            });
            deck.AddCard(new Card()
            {
                Name = "Ataque Médio",
                Cost = 2,
                Damage = 2,
            });
            deck.AddCard(new Card()
            {
                Name = "Ataque Médio 2",
                Cost = 2,
                Damage = 3,
            });
            deck.AddCard(new Card()
            {
                Name = "Ataque FORTE",
                Cost = 3,
                Damage = 5,
            });

            return deck;
        }
    }
}

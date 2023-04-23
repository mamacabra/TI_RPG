using System.Collections.Generic;

namespace Combat
{
    public class DeckFactory
    {
        public DeckFactory(List<Character> heroes, List<Character> enemies)
        {
            foreach (var character in heroes)
            {
                Deck deck = CreateHeroDeck();
                CombatManager.Instance.SetCharacterDeck(character, deck);
            }

            foreach (var character in enemies)
            {
                Deck deck = CreateEnemyDeck();
                CombatManager.Instance.SetCharacterDeck(character, deck);
            }
        }

        private static Deck CreateEnemyDeck()
        {
            Deck deck = new Deck();

            deck.AddCard(new Card()
            {
                Name = "Remove Carta",
                Cost = 1,
                DropTargetCard = 2,
            });
            deck.AddCard(new Card()
            {
                Name = "Remove Carta",
                Cost = 1,
                Damage = 1,
                AddEmptyCard = true,
            });
            deck.AddCard(new Card()
            {
                Name = "Ataque Fraco",
                Cost = 1,
                Damage = 1,
            });
            deck.AddCard(new Card()
            {
                Name = "Ataque Fraco",
                Cost = 1,
                Damage = 1,
            });
            // deck.AddCard(new Card()
            // {
            //     Name = "Ataque Fraco",
            //     Cost = 1,
            //     Damage = 1,
            // });
            // deck.AddCard(new Card()
            // {
            //     Name = "Ataque Fraco 2",
            //     Cost = 1,
            //     Damage = 2,
            // });
            // deck.AddCard(new Card()
            // {
            //     Name = "Ataque Médio 2",
            //     Cost = 2,
            //     Damage = 3,
            // });

            return deck;
        }

        private static Deck CreateHeroDeck()
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
                Name = "Nova Carta",
                Cost = 2,
                Damage = 2,
                DrawCard = 2,
            });
            // deck.AddCard(new Card()
            // {
            //     Name = "Ataque Médio 2",
            //     Cost = 2,
            //     Damage = 3,
            // });
            // deck.AddCard(new Card()
            // {
            //     Name = "Ataque FORTE",
            //     Cost = 3,
            //     Damage = 5,
            // });

            return deck;
        }
    }
}

using System.Collections.Generic;

namespace Combat
{
    public static class DeckFactory
    {
        public static Deck CreateDeck(CharacterType type)
        {
            switch (type)
            {
                case CharacterType.Hero:
                    return CreateHeroDeck();
                case CharacterType.Enemy:
                    return CreateEnemyDeck();
                default:
                    return null;
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

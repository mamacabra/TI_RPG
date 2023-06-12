using System.Collections.Generic;
using System.Linq;

namespace Combat
{
    public class HandCardDisplay
    {
        private readonly List<CardDisplay> _cards;

        public HandCardDisplay()
        {
            _cards = new List<CardDisplay>();
        }

        public void AddCard(CardDisplay card)
        {
            _cards.Add(card);
        }

        public void SetupCardsPosition()
        {
            int middleCard = (_cards.Count -1) / 2;

            if (_cards.Count <= 0) return;

            for (int i = middleCard - 1, p = -1; i >= 0; i--, p--)
                _cards[i].Setup(p);

            _cards[middleCard].Setup();

            for (int i = middleCard + 1, p = 1; i < _cards.Count; i++, p++)
                _cards[i].Setup(p);
        }

        public void UpdateCardList(List<CardController> cards)
        {
            _cards.Clear();
            foreach (CardController card in cards)
            {
                AddCard(card.GetComponent<CardDisplay>());
            }
        }
    }
}

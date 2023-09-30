using System.Collections.Generic;

namespace Combat
{
    public struct Card
    {
        private CardScriptableObject _originalData;
        public string Label { get; private set; }
        public string Description { get; private set; }
        public int Cost { get; private set; }
        public int ActionPointsReceive { get; private set; }
        public int Damage { get; private set; }
        public int Heal { get; private set; }
        public int DrawCard { get; private set; }
        // public int DrawPartyCard { get; private set; }
        public int DropTargetCard { get; private set; }
        public List<CardScriptableObject> AddCards { get; private set; }

        public int StatusBleeding { get; private set; }
        public int StatusPoisoned { get; private set; }

        public Card(CardScriptableObject card)
        {
            _originalData = card;

            Label = card.label;
            Description = card.description;
            Cost = card.cost;
            ActionPointsReceive = card.receive;
            Damage = card.damage;
            Heal = card.heal;
            DrawCard = card.drawCard;
            // DrawPartyCard = card.drawPartyCard;
            DropTargetCard = card.dropCardOnTargetHand;
            AddCards = card.addCardOnTargetDeck;

            StatusBleeding = card.statusBleeding;
            StatusPoisoned = card.statusPoisoned;
        }
    }
}

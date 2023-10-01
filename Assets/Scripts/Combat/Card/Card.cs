using System.Collections.Generic;

namespace Combat
{
    public struct CardStatus
    {
        public bool Bewitch;
        public bool Bleed;
        public bool Burn;
        public bool Confuse;
        public bool Curse;
        public bool Freeze;
        public bool Pierce;
        public bool Poison;
        public bool Reflect;
        public bool Stun;
        public bool Unlucky;
        public bool Vulnerable;
        public bool Weak;
    }

    public struct Card
    {
        public string Label { get; private set; }
        public string Description { get; private set; }

        public int ActionPointsCost { get; private set; }
        public int ActionPointsReceive { get; private set; }

        public int Damage { get; private set; }
        public int Heal { get; private set; }

        public int DrawCard { get; private set; }
        public int DropTargetCard { get; private set; }
        public List<CardScriptableObject> AddTargetCard { get; private set; }
        public CardStatus Status { get; private set; }

        public Card(CardScriptableObject card)
        {
            Label = card.label;
            Description = card.description;

            ActionPointsCost = card.cost;
            ActionPointsReceive = card.receive;

            Damage = card.damage;
            Heal = card.heal;

            DrawCard = card.drawCard;
            DropTargetCard = card.dropCardOnTargetHand;
            AddTargetCard = card.addCardOnTargetDeck;

            Status = new CardStatus
            {
                Bewitch = card.statusBewitch,
                Bleed = card.statusBleed,
                Burn = card.statusBurn,
                Confuse = card.statusConfuse,
                Curse = card.statusCurse,
                Freeze = card.statusFreeze,
                Pierce = card.statusPierce,
                Poison = card.statusPoison,
                Reflect = card.statusReflect,
                Stun = card.statusStun,
                Unlucky = card.statusUnlucky,
                Vulnerable = card.statusVulnerable,
                Weak = card.statusWeak,
            };
        }
    }
}

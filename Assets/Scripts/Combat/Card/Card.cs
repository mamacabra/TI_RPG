using System.Collections.Generic;

namespace Combat
{
    public struct Card
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int ActionPointsReceive { get; set; }
        public int Damage { get; set; }
        public int Heal { get; set; }
        public int DrawCard { get; set; }
        public int DrawPartyCard { get; set; }
        public int DropTargetCard { get; set; }
        public List<CardScriptableObject> AddCards { get; set; }
    }
}

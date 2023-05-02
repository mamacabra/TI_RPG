namespace Combat
{
    public struct Card
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Damage { get; set; }
        public int Heal { get; set; }
        public int DrawCard { get; set; }
        public int DrawPartyCard { get; set; }
        public int DropTargetCard { get; set; }
        public bool AddEmptyCard { get; set; }
    }
}

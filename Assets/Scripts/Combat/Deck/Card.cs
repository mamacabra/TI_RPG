namespace Combat
{
    public struct Card
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Damage { get; set; }
        public int Heal { get; set; }
        public int DrawHeroCard { get; set; }
        public int DrawPartyCard { get; set; }
        public int DropEnemyCard { get; set; }
    }
}

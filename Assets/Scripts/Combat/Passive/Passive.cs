namespace Combat
{
    public abstract class Passive
    {
        public void OnBeforeTurn() {}
        public void OnAfterTurn() {}
        public void OnBeforeAttack() {}
        public void OnAfterAttack() {}
        public void OnBeforeDefend() {}
        public void OnAfterDefend() {}
    }
}

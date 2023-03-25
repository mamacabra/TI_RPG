namespace Combat
{
    public interface ICombatStateObserver
    {
        public void OnCombatStateChanged(CombatStateType state);
    }
}

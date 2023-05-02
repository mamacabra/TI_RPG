namespace Combat
{
    public interface ICharacterObserver
    {
        public void OnCharacterCreated(Character character);
        public void OnCharacterUpdated(Character character);
    }
}

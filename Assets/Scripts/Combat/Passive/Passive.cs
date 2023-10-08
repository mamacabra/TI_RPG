using UnityEngine;

namespace Combat
{
    public abstract class Passive
    {
        public void OnBeforeTurn(Character character) {}
        public void OnAfterTurn(Character character) {}
        public void OnBeforeAttack(Character character) {}
        public void OnAfterAttack(Character character) {}
        public void OnBeforeDefend(Character character) {}
        public void OnAfterDefend(Character character) {}

        protected static bool CalculateChance(int chance)
        {
            return Random.Range(0, 100) < chance;
        }
    }
}

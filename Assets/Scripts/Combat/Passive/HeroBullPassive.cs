using UnityEngine;

namespace Combat
{
    public abstract class HeroBullPassive: Passive
    {
        public new static void OnBeforeTurn()
        {
            Debug.Log("HeroBullPassive.OnBeforeTurn");
        }
    }
}

using UnityEngine;

namespace Combat
{
    public class CardPosition : MonoBehaviour
    {
        private static readonly Vector3 CardInitialPosition = new (-7, -1, 0);
        private static readonly Vector3 Displacement = new (2, 0, 0);

        public void Setup(int position = 0)
        {
            transform.position = CardInitialPosition + Displacement * position;
        }
    }
}

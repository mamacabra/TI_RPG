using UnityEngine;

namespace Combat
{
    public class CardDisplay : MonoBehaviour
    {
        private static readonly Vector3 CardMiddlePosition = new (0, -1, 0);
        private static readonly Vector3 Displacement = new (2, 0, 0);

        [SerializeField] private Vector3 cardPosition;
        private const float Speed = 10f;

        public void Setup(int position = 0)
        {
            cardPosition = CardMiddlePosition + Displacement * position;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, cardPosition) > 0.1f)
            {
                Vector3 direction = cardPosition - transform.position;
                transform.position += direction.normalized * (Speed * Time.deltaTime);
            }
        }
    }
}
using UnityEngine;

namespace Combat
{
    public class CardDisplay : MonoBehaviour
    {
        private static readonly Vector3 CardInitialPosition = new (0, -4, -5f);
        private static readonly Vector3 CardMiddlePosition = new (0, 0.3f, -3f);
        private static readonly Vector3 Displacement = new (1.7f, 0, 0);

        [SerializeField] private Vector3 cardPosition;
        private const float Speed = 10f;

        public void Setup(int position = 0)
        {
            transform.position = CardInitialPosition;
            cardPosition = CardMiddlePosition + Displacement * position;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, cardPosition) > 0.2f)
            {
                Vector3 direction = cardPosition - transform.position;
                transform.position += direction.normalized * (Speed * Time.deltaTime);
            }
        }
    }
}

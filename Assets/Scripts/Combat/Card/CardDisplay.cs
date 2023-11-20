using UnityEngine;

namespace Combat
{
    public class CardDisplay : MonoBehaviour
    {
        private bool firstDisplacement = true;
        private bool isMovement = true;
        private static readonly Vector3 CardInitialPosition = new (0, -4, -5f);
        private static readonly Vector3 CardMiddlePosition = new (0, 0.3f, -3f);
        private static readonly Vector3 Displacement = new (1.7f, 0, 0);

        [SerializeField] private Vector3 cardPosition;
        private const float Speed = 10f;

        public void Setup(int position = 0)
        {
            isMovement = true;

            if (firstDisplacement) {
                firstDisplacement = false;
                transform.position = CardInitialPosition;
            }
            
            cardPosition = CardMiddlePosition + Displacement * position;
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, cardPosition) < 0.2f)
            {
                isMovement = false;
                transform.position = cardPosition;
            }
        }

        private void Update()
        {
            if (isMovement)
            {
                Vector3 direction = cardPosition - transform.position;
                transform.position += direction.normalized * (Speed * Time.deltaTime);
            }
        }
    }
}

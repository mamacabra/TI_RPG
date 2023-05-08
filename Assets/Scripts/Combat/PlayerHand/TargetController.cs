using UnityEngine;

namespace Combat
{
    public class TargetController : MonoBehaviour
    {
        public static TargetController Instance { get; private set; }

        public Member Target { get; private set; }
        private CardController Card { get; set; }

        [Header("VFX")]
        [SerializeField] private GameObject arrowVFX;


        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Card && Target && arrowVFX)
            {
                arrowVFX.SetActive(true);
                VFXLine line = arrowVFX.GetComponent<VFXLine>();
                if (line)
                {
                    line.SetInitialPoint(Card.transform);
                    line.SetTargetPoint(Target.transform);
                }
            }
            else
            {
                arrowVFX.SetActive(false);
            }

            if (Input.GetMouseButton(1))
            {
                RemoveCard();
                RemoveTarget();
            }
        }

        public void SetTarget(Member target)
        {
            Target = target;
        }

        private void RemoveTarget()
        {
            Target = null;
        }

        public void RemoveTarget(Member target)
        {
            if (Target == target) RemoveTarget();
        }

        public void SetCard(CardController cardController)
        {
            Card = cardController;
            VFXSelected.SetClickedStriker(Card.Owner);
        }

        public void RemoveCard()
        {
            Card = null;
            VFXSelected.SetClickedStriker(null);
        }
    }
}

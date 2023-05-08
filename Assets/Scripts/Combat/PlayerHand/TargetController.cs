using UnityEngine;

namespace Combat
{
    public class TargetController : MonoBehaviour
    {
        public static TargetController Instance { get; private set; }

        [Header("Card and Target")]
        [SerializeField] private CardController card;
        [SerializeField] private Member target;

        [Header("VFX")]
        [SerializeField] private GameObject arrowVFX;

        public Member Target => target;
        private Member Owner => card.Owner;
        private Card Card => card.Card;
        private bool HasSelectedCard => card != null;
        public bool HasTarget => target != null;

        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            if (card && target && arrowVFX)
            {
                arrowVFX.SetActive(true);
                VFXLine line = arrowVFX.GetComponent<VFXLine>();
                if (line)
                {
                    line.SetInitialPoint(card.transform);
                    line.SetTargetPoint(target.transform);
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
            this.target = target;
        }

        private void RemoveTarget()
        {
            target = null;
        }

        public void RemoveTarget(Member target)
        {
            if (this.target == target) RemoveTarget();
        }

        public void SetCard(CardController cardController)
        {
            card = cardController;
            VFXSelected.SetClickedStriker(card.Owner);
        }

        public void RemoveCard()
        {
            card = null;
            VFXSelected.SetClickedStriker(null);
        }

        public void UseCard()
        {
            card.UseCard();
        }
    }
}

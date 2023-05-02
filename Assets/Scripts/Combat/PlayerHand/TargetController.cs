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

        private Member Owner => card.Owner;
        private Card Card => card.Card;

        public bool HasCardSelected => card != null;

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
            if (this.target == target) this.target = null;
        }

        public void SetCard(CardController card)
        {
            this.card = card;
        }

        private void RemoveCard()
        {
            card = null;
        }

        public void UseCard()
        {
            if (card && target) Owner.UseCard(Card, target);

            RemoveCard();
            RemoveTarget();
        }
    }
}

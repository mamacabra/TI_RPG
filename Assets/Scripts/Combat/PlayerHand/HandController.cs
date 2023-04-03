using UnityEngine;

namespace Combat
{
    public class HandController : MonoBehaviour
    {
        public static HandController Instance { get; private set; }

        [SerializeField] private Character target;
        [SerializeField] private Card3D selectedCard;
        [SerializeField] private GameObject vfx;

        public bool HasCardSelected => selectedCard != null;

        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            if (selectedCard && target)
            {
                vfx.SetActive(true);
                VFXLine line = vfx.GetComponent<VFXLine>();
                if (line)
                {
                    line.SetInitialPoint(selectedCard.transform);
                    line.SetTargetPoint(target.transform);
                }
            }
            else vfx.SetActive(false);
        }

        public void SetTarget(Character target)
        {
            this.target = target;
        }

        public void RemoveTarget(Character target)
        {
            if (this.target == target) this.target = null;
        }

        private void RemoveTarget()
        {
            target = null;
        }

        public void SetCard(Card3D card)
        {
            selectedCard = card;
        }

        private void RemoveCard()
        {
            selectedCard = null;
        }

        public void UseCard()
        {
            if (selectedCard && target)
                CombatManager.UseCard(selectedCard.character, selectedCard.card, target);

            RemoveCard();
            RemoveTarget();
        }
    }
}

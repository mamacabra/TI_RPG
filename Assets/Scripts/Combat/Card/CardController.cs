using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CardAttributes))]
    [RequireComponent(typeof(CardDisplay))]
    [RequireComponent(typeof(CardVFX))]
    public class CardController : MonoBehaviour
    {
        private Card Card { get; set; }
        private CardVFX _cardVFX;

        public Member Owner { get; private set; }
        public CardDisplay Display { get; private set; }

        private void Start()
        {
            _cardVFX = GetComponent<CardVFX>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0)) UseCard();
        }

        private void OnMouseOver()
        {
            _cardVFX.SetOverMaterial();
            VFXSelected.SetHoveredStriker(Owner);
        }

        private void OnMouseExit()
        {
            _cardVFX.SetDefaultMaterial();
            VFXSelected.SetHoveredStriker(null);
        }

        private void OnMouseDown()
        {
            TargetController.Instance.SetCard(this);
        }

        private void OnMouseUp()
        {
            UseCard();
        }

        public void Setup(Member member, Card card)
        {
            Owner = member;
            Card = card;
            Display = GetComponent<CardDisplay>();

            CardAttributes cardAttributes = GetComponent<CardAttributes>();
            cardAttributes.Setup(card);
        }

        private void UseCard()
        {
            if (!Owner.Character.HasEnoughActionPoints(Card.Cost) || !TargetController.Instance.Target) return;

            Member target = TargetController.Instance.Target;

            CardBehavior.Use(Owner, Card, target);
            HandController.Instance.RemoveUsedCard(this);
            Owner.Hand.Remove(Card);

            TargetController.Instance.RemoveCard();
            TargetController.Instance.RemoveTarget(target);
        }
    }
}

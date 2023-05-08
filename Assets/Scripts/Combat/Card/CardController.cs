using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CardAttributes))]
    [RequireComponent(typeof(CardDisplay))]
    public class CardController : MonoBehaviour
    {
        public Member Owner { get; private set; }
        public Card Card { get; private set; }
        public CardDisplay Display { get; private set; }

        public void Setup(Member member, Card card)
        {
            Owner = member;
            Card = card;
            Display = GetComponent<CardDisplay>();

            CardAttributes cardAttributes = GetComponent<CardAttributes>();
            cardAttributes.Setup(card);
        }

        private void OnMouseOver()
        {
            VFXSelected.SetHoveredStriker(Owner);
        }

        private void OnMouseExit()
        {
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

        public void UseCard()
        {
            if (Owner.Character.HasEnoughActionPoints(Card.Cost) && TargetController.Instance.HasTarget)
            {
                Member target = TargetController.Instance.Target;

                CardBehavior.Use(Owner, Card, target);
                HandController.Instance.RemoveUsedCard(this);
                Owner.Hand.Remove(Card);

                TargetController.Instance.RemoveCard();
                TargetController.Instance.RemoveTarget(target);
            }
        }
    }
}

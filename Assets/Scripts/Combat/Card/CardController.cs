using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CardAttributes))]
    [RequireComponent(typeof(CardDisplay))]
    [RequireComponent(typeof(CardVFX))]
    public class CardController : MonoBehaviour
    {
        private Card Card { get; set; }
        private Member Striker { get; set; }

        private CardVFX _cardVFX;

        public CardDisplay Display { get; private set; }

        public static CardController ClickedCard { get; private set; }
        public static CardController DraggedCard { get; private set; }

        private void Start()
        {
            _cardVFX = GetComponent<CardVFX>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && ClickedCard == this)
            {
                UseCard(ClickedCard.Card);
            }

            if (Input.GetMouseButton(1))
            {
                ClearSelectedCards();
            }
        }

        private void OnMouseOver()
        {
            _cardVFX.SetOverMaterial();
            CharacterCardSelectedVFX.SetHoveredStriker(Striker);
        }

        private void OnMouseExit()
        {
            _cardVFX.SetDefaultMaterial();
            CharacterCardSelectedVFX.SetHoveredStriker(null);
        }

        private void OnMouseDrag()
        {
            DraggedCard = this;
        }


        private void OnMouseDown()
        {
            CharacterCardSelectedVFX.SetClickedStriker(Striker);
            ClickedCard = this;
        }

        private void OnMouseUp()
        {
            if (DraggedCard == this)
            {
                UseCard(DraggedCard.Card);
            }
        }

        public void Setup(Member member, Card card)
        {
            Striker = member;
            Card = card;
            Display = GetComponent<CardDisplay>();

            CardAttributes cardAttributes = GetComponent<CardAttributes>();
            cardAttributes.Setup(card);
        }

        private void UseCard(Card card)
        {
            bool hasActionPoints = Striker.Character.HasEnoughActionPoints(card.Cost);
            Member target = Target.HoveredTarget;

            if (hasActionPoints == false || target is null) return;

            CardBehavior.Use(Striker, card, target);
            HandController.Instance.RemoveUsedCard(this);
            Striker.Hand.Remove(card);

            ClearSelectedCards();
        }

        private static void ClearSelectedCards()
        {
            ClickedCard = null;
            DraggedCard = null;
        }
    }
}

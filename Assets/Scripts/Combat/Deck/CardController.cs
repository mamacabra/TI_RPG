using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(CardAttributes))]
    [RequireComponent(typeof(CardPosition))]
    public class CardController : MonoBehaviour
    {
        public Member Member { get; private set; }
        public Card Card { get; private set; }

        public void Setup(Member member, Card card, int position)
        {
            Member = member;
            Card = card;

            CardAttributes cardAttributes = GetComponent<CardAttributes>();
            cardAttributes.Setup(card);

            CardPosition cardPosition = GetComponent<CardPosition>();
            cardPosition.Setup(position);
        }

        private void OnMouseDown()
        {
            TargetController.Instance.SetCard(Member, this);
        }

        private void OnMouseUp()
        {
            TargetController.Instance.UseCard();
        }
    }
}

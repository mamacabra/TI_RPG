using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class CardAttributes : MonoBehaviour
    {
        [Header("Card Texts")]
        [SerializeField] private Text title;
        [SerializeField] private Text description;

        [Header("Card Attributes")]
        [SerializeField] private Text cost;
        [SerializeField] private Text damage;
        [SerializeField] private Text heal;

        public void Setup(Card card)
        {
            title.text = card.Label;
            description.text = card.Description;
            cost.text = card.ActionPointsCost.ToString();

            SetupDescription(card);
            SetupAttributes(card);
        }

        private void SetupDescription(Card card)
        {

            if (card.ActionPointsCost > 0)
            {
                description.text = "Custo (" + card.ActionPointsCost + ") pontos de ação. ";
            }

            if (card.Damage > 0)
            {
                description.text += "Causa (" + card.Damage + ") de dano. ";
            }

            if (card.Heal > 0)
            {
                description.text += "Cura (" + card.Heal + ") pontos de vida. ";
            }

            if (card.ActionPointsReceive > 0)
            {
                description.text += "Recebe (" + card.ActionPointsReceive + ") pontos de ação. ";
            }

            if (card.DrawCard > 0)
            {
                description.text += "Saca (" + card.DrawCard + ") cartas. ";
            }

            if (card.AddTargetCard.Count > 0)
            {
                description.text += "Adiciona (" + card.AddTargetCard.Count + ") cartas do alvo. ";
            }

            if (card.DropTargetCard > 0)
            {
                description.text += "Remove (" + card.DropTargetCard + ") cartas do alvo. ";
            }
        }

        private void SetupAttributes(Card card)
        {
            if (card.Damage != 0) damage.text = card.Damage.ToString();
            else damage.gameObject.SetActive(false);

            if (card.Heal != 0) heal.text = card.Heal.ToString();
            else heal.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Combat
{
    public class CardAttributes : MonoBehaviour
    {
        [Header("Card Texts")]
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Image image;

        [Header("Card Attributes")]
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private TextMeshProUGUI damage;
        [SerializeField] private TextMeshProUGUI heal;

        public void Setup(Card card)
        {
            title.text = card.Label;
            description.text = card.Description;
            cost.text = card.ActionPointsCost.ToString();

            SetupSprite(card);
            SetupDescription(card);
            SetupAttributes(card);
        }

        private void SetupSprite(Card card)
        {
            if (card.Thumb && image)
            {
                image.sprite = card.Thumb;
            }
        }

        private void SetupDescription(Card card)
        {

            // if (card.ActionPointsCost > 0)
            // {
            //     description.text = "Custo (" + card.ActionPointsCost + ") pontos de ação. ";
            // }
            //
            // if (card.Damage > 0)
            // {
            //     description.text += "Causa (" + card.Damage + ") de dano. ";
            // }
            //
            // if (card.Heal > 0)
            // {
            //     description.text += "Cura (" + card.Heal + ") pontos de vida. ";
            // }

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

            if (card.Status.Bleed)
            {
                description.text += "Sangramento. \n";
            }

            if (card.Status.Confuse)
            {
                description.text += "Confusão. \n";
            }

            if (card.Status.Reflect)
            {
                description.text += "Reflete. \n";
            }

            if (card.Status.Stun)
            {
                description.text += "Atordoamento. \n";
            }

            if (card.Status.Weak)
            {
                description.text += "Enfraquecimento. \n";
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

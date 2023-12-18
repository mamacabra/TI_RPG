using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Combat;
using UnityEngine.UI;

public class InventoryCard : MonoBehaviour
{
    [Header("Card Texts")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image itemSprite;

    [Header("Card Attributes")]
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI heal;

    public void Setup(CardScriptableObject card)
    {
        title.text = card.label;
        description.text = card.description;
        cost.text = card.cost.ToString();
        itemSprite.sprite = card.sprite;

        SetupDescription(card);
        SetupAttributes(card);
    }

    private void SetupDescription(CardScriptableObject card)
    {

        // if (card.cost > 0)
        // {
        //     description.text = "Custo (" + card.cost + ") pontos de ação. ";
        // }
        //
        // if (card.damage > 0)
        // {
        //     description.text += "Causa (" + card.damage + ") de dano. ";
        // }
        //
        // if (card.heal > 0)
        // {
        //     description.text += "Cura (" + card.heal + ") pontos de vida. ";
        // }
        //
        if (card.receive > 0)
        {
            description.text += "Recebe (" + card.receive + ") pontos de ação. ";
        }

        if (card.drawCard > 0)
        {
            description.text += "Saca (" + card.drawCard + ") cartas. ";
        }

        if (card.addCardOnTargetDeck.Count > 0)
        {
            description.text += "Adiciona (" + card.addCardOnTargetDeck.Count + ") cartas do alvo. ";
        }

        if (card.dropCardOnTargetHand > 0)
        {
            description.text += "Remove (" + card.dropCardOnTargetHand + ") cartas do alvo. ";
        }

        if (card.statusBleed)
        {
            description.text += "Sangramento. \n";
        }

        if (card.statusConfuse)
        {
            description.text += "Confusão. \n";
        }

        if (card.statusReflect)
        {
            description.text += "Reflete. \n";
        }

        if (card.statusStun)
        {
            description.text += "Atordoamento. \n";
        }

        if (card.statusWeak)
        {
            description.text += "Enfraquecimento. \n";
        }
    }

    private void SetupAttributes(CardScriptableObject card)
    {
        if (card.damage != 0) damage.text = card.damage.ToString();
        else damage.transform.parent.gameObject.SetActive(false);

        if (card.heal != 0) heal.text = card.heal.ToString();
        else heal.transform.parent.gameObject.SetActive(false);
    }
}

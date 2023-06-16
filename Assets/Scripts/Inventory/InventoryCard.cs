using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Combat;

public class InventoryCard : MonoBehaviour
{
    [Header("Card Texts")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    [Header("Card Attributes")]
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI heal;

    public void Setup(CardScriptableObject card)
    {
        title.text = card.label;
        description.text = card.description;
        cost.text = card.cost.ToString();

        SetupDescription(card);
        SetupAttributes(card);
    }

    private void SetupDescription(CardScriptableObject card)
    {

        if (card.cost > 0)
        {
            description.text = "Custo (" + card.cost + ") pontos de ação. ";
        }

        if (card.damage > 0)
        {
            description.text += "Causa (" + card.damage + ") de dano. ";
        }

        if (card.heal > 0)
        {
            description.text += "Cura (" + card.heal + ") pontos de vida. ";
        }

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
    }

    private void SetupAttributes(CardScriptableObject card)
    {
        if (card.damage != 0) damage.text = card.damage.ToString();
        else damage.gameObject.SetActive(false);

        if (card.heal != 0) heal.text = card.heal.ToString();
        else heal.gameObject.SetActive(false);
    }
}
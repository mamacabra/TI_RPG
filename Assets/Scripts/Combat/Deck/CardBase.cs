using UnityEngine;
using UnityEngine.UI;

public class CardBase : MonoBehaviour
{
    public Text cardTitle;
    public Text cardCost;
    public Text cardDamage;
    public Text cardHeal;

    static Vector3 CardSize = new Vector3(60, 75, 0);
    static Vector3 CardSpacing = new Vector3(10, 0, 0);
    static Vector3 CardInitialPosition = new Vector3(-330, -120, 0);
    static Vector3 Displacement = new Vector3(CardSize.x, 0, 0) + CardSpacing;

    public void SetupCard(Card card, int position = 0)
    {
        SetupCardAttributes(card);
        SetupCardPosition(position);
    }

    private void SetupCardAttributes(Card card)
    {
        cardTitle.text = card.Name;
        cardCost.text = "Cost: " + card.Cost.ToString();
        cardDamage.text = "Damange: " + card.Damage.ToString();
        cardHeal.text = "Heal: " + card.Heal.ToString();
    }

    private void SetupCardPosition(int position = 0)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.localPosition = CardInitialPosition + Displacement * position;
    }
}

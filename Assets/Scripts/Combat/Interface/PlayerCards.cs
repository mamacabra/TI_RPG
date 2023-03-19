using UnityEngine;

public class PlayerCards : MonoBehaviour
{
    public GameObject cardPrefab;

    private void Start()
    {
        int cardPosition = 0;
        foreach (var character in CombatManager.Instance.characters)
        {
            foreach (var card in character.hand)
            {
                GameObject gb = Instantiate(cardPrefab, transform);
                CardBase cardBase = gb.GetComponent<CardBase>();
                cardBase.SetupCard(card, cardPosition);
                cardPosition++;
            }
        }
    }
}

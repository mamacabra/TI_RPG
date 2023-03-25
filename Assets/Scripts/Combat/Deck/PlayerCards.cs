using UnityEngine;

namespace Combat
{
    public class PlayerCards : MonoBehaviour
    {
        public GameObject cardPrefab;

        // private void Start()
        // {
        //     int cardIndex = 0;
        //     foreach (var character in CombatManager.Instance.heroes)
        //     {
        //         foreach (var card in character.hand)
        //         {
        //             GameObject gb = Instantiate(cardPrefab, transform);
        //             CardBase cardBase = gb.GetComponent<CardBase>();
        //             cardBase.Setup(character, card, cardIndex);
        //             cardIndex++;
        //         }
        //     }
        // }
    }
}

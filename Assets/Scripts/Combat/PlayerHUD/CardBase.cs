using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat
{
    public class CardBase : MonoBehaviour
    {
        public Text cardTitle;
        public Text cardCost;
        public Text cardDamage;
        public Text cardHeal;

        private static readonly Vector3 CardSize = new Vector3(60, 75, 0);
        private static readonly Vector3 CardSpacing = new Vector3(10, 0, 0);
        private static readonly Vector3 CardInitialPosition = new Vector3(-330, -120, 0);
        private static readonly Vector3 Displacement = new Vector3(CardSize.x, 0, 0) + CardSpacing;

        public void Setup(Character character, Card card, int position = 0)
        {
            SetupCardAttributes(card);
            SetupCardPosition(position);
            AddClickEvent(character, card);
        }

        private void SetupCardAttributes(Card card)
        {
            cardTitle.text = card.Name;
            cardCost.text = "Cost: " + card.Cost.ToString();
            cardDamage.text = "Damage: " + card.Damage.ToString();
            cardHeal.text = "Heal: " + card.Heal.ToString();
        }

        private void SetupCardPosition(int position = 0)
        {
            RectTransform rect = GetComponent<RectTransform>();
            rect.localPosition = CardInitialPosition + Displacement * position;
        }

        private void AddClickEvent(Character character, Card card)
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                List<Character> targets = new List<Character>();
                foreach (var target in CombatManager.Instance.Enemies)
                {
                    if (target.character.isDead == false) targets.Add(target.character);
                }
                int r = Random.Range(0, targets.Count);

                CombatManager.UseCard(character, card, targets[r]);
            });
        }
    }
}

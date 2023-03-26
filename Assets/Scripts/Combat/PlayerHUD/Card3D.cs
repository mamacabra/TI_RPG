using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Combat
{
    public class Card3D : MonoBehaviour
    {
        public Text cardTitle;
        public Text cardCost;
        public Text cardDamage;
        public Text cardHeal;

        private Character character;
        private Card card;

        private static readonly Vector3 CardInitialPosition = new (-7, -1, 0);
        private static readonly Vector3 Displacement = new (2, 0, 0);

        public void Setup(Character character, Card card, int position = 0)
        {
            this.character = character;
            this.card = card;

            SetupCardAttributes(card);
            SetupCardPosition(position);
        }

        private void SetupCardAttributes(Card card)
        {
            cardTitle.text = card.Name;
            cardCost.text = "Cost: " + card.Cost;
            cardDamage.text = "Damage: " + card.Damage;
            cardHeal.text = "Heal: " + card.Heal;
        }

        private void SetupCardPosition(int position = 0)
        {
            transform.position = CardInitialPosition + Displacement * position;
        }

        private void OnMouseUp()
        {
            List<Character> targets = new List<Character>();
            foreach (var target in CombatManager.Instance.Enemies)
            {
                if (target.character.IsDead == false) targets.Add(target.character);
            }
            int r = Random.Range(0, targets.Count);

            CombatManager.UseCard(character, card, targets[r]);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace Combat.Helpers
{
    internal enum CharacterTurn
    {
        Hero,
        Enemy,
    }

    public class ButtonEndTurnHelper : MonoBehaviour
    {
        [SerializeField] private CharacterTurn characterTurn = CharacterTurn.Hero;

        public void Start()
        {
            AddClickListener();
        }

        private void AddClickListener()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(EndTurn);
        }

        private void EndTurn()
        {
            switch (characterTurn)
            {
                case CharacterTurn.Hero:
                    CombatState.Instance.SetState(CombatStateType.HeroDeckShuffle);
                    break;
                case CharacterTurn.Enemy:
                    CombatState.Instance.SetState(CombatStateType.EnemyDeckShuffle);
                    break;
            }
        }
    }
}

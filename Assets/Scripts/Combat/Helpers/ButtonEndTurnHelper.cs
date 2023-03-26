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

        private static void EndTurn()
        {
            CombatState.Instance.NextState();
        }
    }
}

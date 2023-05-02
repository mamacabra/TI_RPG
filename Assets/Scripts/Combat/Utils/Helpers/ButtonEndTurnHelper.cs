using UnityEngine;
using UnityEngine.UI;

namespace Combat.Helpers
{
    public class ButtonEndTurnHelper : MonoBehaviour
    {
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

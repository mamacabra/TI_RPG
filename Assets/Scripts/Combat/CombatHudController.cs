using UnityEngine;

namespace Combat
{
    public class CombatHudController : MonoBehaviour, ICombatStateObserver
    {
        [Header("Turn Combat Panels")]
        [SerializeField] private GameObject playerHUD;
        [SerializeField] private GameObject enemyHUD;

        [Header("End Combat Panels")]
        [SerializeField] private GameObject victoryModal;
        [SerializeField] private GameObject defeatModal;

        [Header("Other Panels")]
        [SerializeField] private GameObject playerCards;

        public void Start()
        {
            HiddenAllPanels();
            CombatState.Instance.Subscribe(this);
        }

        public void OnCombatStateChanged(CombatStateType state)
        {
            HiddenAllPanels();

            switch (state)
            {
                case CombatStateType.HeroTurn:
                    ShowPanel(playerHUD);
                    ShowPanel(playerCards);
                    break;
                case CombatStateType.EnemyTurn:
                    ShowPanel(enemyHUD);
                    break;
                case CombatStateType.Victory:
                    ShowPanel(victoryModal);
                    break;
                case CombatStateType.Defeat:
                    ShowPanel(defeatModal);
                    break;
            }
        }

        private void HiddenAllPanels()
        {
            HiddenPanel(playerHUD);
            HiddenPanel(enemyHUD);

            HiddenPanel(victoryModal);
            HiddenPanel(defeatModal);

            HiddenPanel(playerCards);
        }

        private static void ShowPanel(GameObject panel)
        {
            panel.SetActive(true);
        }

        private static void HiddenPanel(GameObject panel)
        {
            panel.SetActive(false);
        }
    }
}

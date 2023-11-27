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
        [SerializeField] private GameObject heroHealthBars;
        [SerializeField] private GameObject enemyHealthBars;

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
                    HiddenPanel(heroHealthBars);
                    HiddenPanel(enemyHealthBars);
                    ShowPanel(victoryModal);
                    break;
                case CombatStateType.Defeat:
                    HiddenPanel(heroHealthBars);
                    HiddenPanel(enemyHealthBars);
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
            if (panel) panel.SetActive(true);
        }

        private static void HiddenPanel(GameObject panel)
        {
            if (panel) panel.SetActive(false);
        }
    }
}

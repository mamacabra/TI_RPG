using UnityEngine;

public class HudController : MonoBehaviour, ICombatStateObserver
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
        CombatState.Instance.AddObserver(this);
    }

    public void Notify(CombatStateType state)
    {
        switch (state)
        {
            case CombatStateType.PlayerTurn:
                ShowPlayerHUD();
                break;
            case CombatStateType.EnemyTurn:
                ShowEnemyHUD();
                break;
            case CombatStateType.Victory:
                ShowVictoryModal();
                break;
            case CombatStateType.Defeat:
                ShowDefeatModal();
                break;
        }
    }

    private void HiddenAllPanels()
    {
        playerHUD.SetActive(false);
        enemyHUD.SetActive(false);

        victoryModal.SetActive(false);
        defeatModal.SetActive(false);

        playerCards.SetActive(false);
    }

    private void ShowPlayerHUD()
    {
        HiddenAllPanels();
        playerHUD.SetActive(true);
        playerCards.SetActive(true);
    }

    private void ShowEnemyHUD()
    {
        HiddenAllPanels();
        enemyHUD.SetActive(true);
    }

    private void ShowVictoryModal()
    {
        HiddenAllPanels();
        victoryModal.SetActive(true);
    }

    private void ShowDefeatModal()
    {
        HiddenAllPanels();
        defeatModal.SetActive(true);
    }
}

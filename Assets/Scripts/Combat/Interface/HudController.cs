using UnityEngine;

public class HudController : MonoBehaviour
{
    public static HudController Instance;

    [Header("Turn Combat Panels")]
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private GameObject enemyHUD;

    [Header("End Combat Panels")]
    [SerializeField] private GameObject victoryModal;
    [SerializeField] private GameObject defeatModal;

    [Header("Other Panels")]
    [SerializeField] private GameObject playerCards;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
    }

    private void HiddenAllPanels()
    {
        playerHUD.SetActive(false);
        enemyHUD.SetActive(false);

        victoryModal.SetActive(false);
        defeatModal.SetActive(false);

        playerCards.SetActive(false);
    }

    public void ShowPlayerHUD()
    {
        HiddenAllPanels();
        playerHUD.SetActive(true);
        playerCards.SetActive(true);
    }

    public void ShowEnemyHUD()
    {
        HiddenAllPanels();
        enemyHUD.SetActive(true);
    }

    public void ShowVictoryModal()
    {
        HiddenAllPanels();
        victoryModal.SetActive(true);
    }

    public void ShowDefeatModal()
    {
        HiddenAllPanels();
        defeatModal.SetActive(true);
    }
}

using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Singleton
    public static UIManager instance { get; private set; }

    [SerializeField]
    private GameObject mainMenuUIObj, gameOverUIObj, gunUIObj;

    [SerializeField]
    private TextMeshProUGUI levelText, moneyText, gameOverStateText, gameOverButtonText, newGunUnlockedText;

    private const string LEVEL_COMPLETED_STATE_TEXT = "Level Completed";
    private const string LEVEL_FAILED_STATE_TEXT = "Level Failed";

    private const string LEVEL_COMPLETED_BUTTON_TEXT = "Next Level";
    private const string LEVEL_FAILED_BUTTON_TEXT = "Restart Level";

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
    }

    public void CloseMainMenuUI()
    {
        mainMenuUIObj.SetActive(false);
        gunUIObj.SetActive(true);
    }

    public void CloseInGameUI()
    {
        gunUIObj.SetActive(false);
    }
    
    public void ShowGameOverUI(bool isLevelCompleted)
    {
        gameOverUIObj.SetActive(true);

        if (isLevelCompleted)
        {
            gameOverStateText.SetText(LEVEL_COMPLETED_STATE_TEXT);
            gameOverButtonText.SetText(LEVEL_COMPLETED_BUTTON_TEXT);
        }
        else
        {
            gameOverStateText.SetText(LEVEL_FAILED_STATE_TEXT);
            gameOverButtonText.SetText(LEVEL_FAILED_BUTTON_TEXT);
        }
    }

    public void UpdateMoneyText()
    {
        moneyText.SetText(Player.instance.GetCurrentMoney().ToString());
    }

    public void UpdateLevelText()
    {
        levelText.SetText( $"Level {LevelManager.GetLevelIndex() + 1}" ); //levelIndex starts from 0, therefore +1 added
    }

    public void UpdateNewGunUnlockedText(GunType gunType)
    {
        newGunUnlockedText.SetText($"New gun unlocked: {gunType.ToString()}");
    }
}
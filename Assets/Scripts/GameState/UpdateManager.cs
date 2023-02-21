using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateManager : MonoBehaviour
{
    [SerializeField]
    private Button upgradeFireDamageButton, upgradeFireRangeButton, upgradeStarterHealthButton, upgradeMoneyEarningButton;

    // Set these as buttons' first TextMeshProUGUI components
    private TextMeshProUGUI upgradeFireDamageButtonText, upgradeFireRangeButtonText, upgradeStarterHealthButtonText, upgradeMoneyEarningButtonText;

    private const float FIRE_DAMAGE_UPGRADE_AMOUNT = .1f;
    private const float FIRE_RANGE_UPGRADE_AMOUNT = .1f;
    private const int STARTER_HEALTH_UPGRADE_AMOUNT = 1;
    private const float MONEY_EARNING_UPGRADE_AMOUNT = 1;

    private int upgradeFireDamagePrice, upgradeFireRangePrice, upgradeStarterHealthPrice, upgradeMoneyEarningPrice;
    private const float PRICE_MULTIPLIER = 2f;

    void Start()
    {
        upgradeFireDamageButton.onClick.AddListener(UpgradeFireDamage);
        upgradeFireDamageButtonText = upgradeFireDamageButton.GetComponentInChildren<TextMeshProUGUI>();

        upgradeFireRangeButton.onClick.AddListener(UpgradeFireRange);
        upgradeFireRangeButtonText = upgradeFireRangeButton.GetComponentInChildren<TextMeshProUGUI>();

        upgradeStarterHealthButton.onClick.AddListener(UpgradeStarterHealth);
        upgradeStarterHealthButtonText = upgradeStarterHealthButton.GetComponentInChildren<TextMeshProUGUI>();

        upgradeMoneyEarningButton.onClick.AddListener(UpgradeMoneyEarning);
        upgradeMoneyEarningButtonText = upgradeMoneyEarningButton.GetComponentInChildren<TextMeshProUGUI>();

        SetUpgradeElement(ref upgradeFireDamagePrice, Gun.damageMultiplier, GameData.INITIAL_DAMAGE_MULTIPLIER,
                            FIRE_DAMAGE_UPGRADE_AMOUNT, upgradeFireDamageButton, upgradeFireDamageButtonText);

        SetUpgradeElement(ref upgradeFireRangePrice, Gun.rangeMultiplier, GameData.INITIAL_RANGE_MULTIPLIER,
                            FIRE_RANGE_UPGRADE_AMOUNT, upgradeFireRangeButton, upgradeFireRangeButtonText);

        SetUpgradeElement(ref upgradeStarterHealthPrice, Player.instance.GetStarterHealth(), GameData.INITIAL_STARTER_HEALTH,
                            STARTER_HEALTH_UPGRADE_AMOUNT, upgradeStarterHealthButton, upgradeStarterHealthButtonText);

        SetUpgradeElement(ref upgradeMoneyEarningPrice, Player.instance.GetMoneyMultiplier(), GameData.INITIAL_MONEY_MULTIPLIER,
                            MONEY_EARNING_UPGRADE_AMOUNT, upgradeMoneyEarningButton, upgradeMoneyEarningButtonText);

        UpdateButtonsInteractable();
    }

    // Enable/disable upgrade buttons with player's money and game limits respectively
    private void UpdateButtonsInteractable()
    {
        if (upgradeFireDamagePrice > Player.instance.GetCurrentMoney())
        {
            upgradeFireDamageButton.interactable = false;
        }

        if (upgradeFireRangePrice > Player.instance.GetCurrentMoney())
        {
            upgradeFireRangeButton.interactable = false;
        }

        if (upgradeStarterHealthPrice > Player.instance.GetCurrentMoney())
        {
            upgradeStarterHealthButton.interactable = false;
        }
        else if (Player.instance.GetStarterHealth() >= Player.instance.GetMaxLevel())
        {
            upgradeStarterHealthButton.interactable = false;
            upgradeStarterHealthButtonText.SetText("Level limit");
        }

        if (upgradeMoneyEarningPrice > Player.instance.GetCurrentMoney())
        {
            upgradeMoneyEarningButton.interactable = false;
        }
    }

    #region Button functions

    private void UpgradeFireDamage()
    {
        Player.instance.TakeMoney(upgradeFireDamagePrice);
        Gun.IncreaseDamageMultiplier(FIRE_DAMAGE_UPGRADE_AMOUNT);

        SetUpgradeElement(ref upgradeFireDamagePrice, Gun.damageMultiplier, GameData.INITIAL_DAMAGE_MULTIPLIER,
                            FIRE_DAMAGE_UPGRADE_AMOUNT, upgradeFireDamageButton, upgradeFireDamageButtonText);
    }

    private void UpgradeFireRange()
    {
        Player.instance.TakeMoney(upgradeFireRangePrice);
        Gun.IncreaseRangeMultiplier(FIRE_RANGE_UPGRADE_AMOUNT);

        SetUpgradeElement(ref upgradeFireRangePrice, Gun.rangeMultiplier, GameData.INITIAL_RANGE_MULTIPLIER,
                            FIRE_RANGE_UPGRADE_AMOUNT, upgradeFireRangeButton, upgradeFireRangeButtonText);
    }

    private void UpgradeStarterHealth()
    {
        Player.instance.TakeMoney(upgradeStarterHealthPrice);
        Player.instance.IncreaseStarterHealth(STARTER_HEALTH_UPGRADE_AMOUNT);

        SetUpgradeElement(ref upgradeStarterHealthPrice, Player.instance.GetStarterHealth(), GameData.INITIAL_STARTER_HEALTH,
                            STARTER_HEALTH_UPGRADE_AMOUNT, upgradeStarterHealthButton, upgradeStarterHealthButtonText);

        if (Player.instance.GetStarterHealth() >= Player.instance.GetMaxLevel())
        {
            upgradeStarterHealthButton.interactable = false;
            upgradeStarterHealthButtonText.SetText("Level limit");
        }
    }

    private void UpgradeMoneyEarning()
    {
        Player.instance.TakeMoney(upgradeMoneyEarningPrice);
        Player.instance.IncreaseMoneyMultiplier(MONEY_EARNING_UPGRADE_AMOUNT);

        SetUpgradeElement(ref upgradeMoneyEarningPrice, Player.instance.GetMoneyMultiplier(), GameData.INITIAL_MONEY_MULTIPLIER,
                            MONEY_EARNING_UPGRADE_AMOUNT, upgradeMoneyEarningButton, upgradeMoneyEarningButtonText);
    }

    #endregion

    private void SetUpgradeElement(ref int price, int currentValue, int initialValue, int upgradeAmount, Button upgradeButton, TextMeshProUGUI upgradeText)
    {
        price = (int)Mathf.Pow(PRICE_MULTIPLIER, (currentValue - initialValue) / upgradeAmount);
        upgradeText.SetText($"{price}");

        UpdateButtonsInteractable();
    }

    private void SetUpgradeElement(ref int price, float currentValue, float initialValue, float upgradeAmount, Button upgradeButton, TextMeshProUGUI upgradeText)
    {
        price = (int)Mathf.Pow(PRICE_MULTIPLIER, (currentValue - initialValue) / upgradeAmount);
        upgradeText.SetText($"{price}");
 
        UpdateButtonsInteractable();
    }
}
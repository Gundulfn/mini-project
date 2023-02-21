using UnityEngine;

public static class GameData
{
    public const int INITIAL_UNLOCKED_GUN_COUNT = 1;
    public const int INITIAL_STARTER_HEALTH = 1;

    public const float INITIAL_DAMAGE_MULTIPLIER = 1;
    public const float INITIAL_RANGE_MULTIPLIER = 1;

    public const int INITIAL_MONEY_AMOUNT = 0;
    public const float INITIAL_MONEY_MULTIPLIER = 1;

    public const int INITIAL_LEVEL_INDEX = 0;

    // Load by checking saved values
    // if a saved value are not appropriate, set its initial value
    public static void LoadPlayerData(ref Health health, ref Money money, ref int unlockedGunCount)
    {
        // Limit unlocked gun count with maximum possible gun count
        if(PlayerPrefs.GetInt("unlockedGunCount") > GunData.GUN_UPGRADE_COUNT)
        {
            unlockedGunCount = GunData.GUN_UPGRADE_COUNT;
        }
        else
        {
            unlockedGunCount = (PlayerPrefs.GetInt("unlockedGunCount") >= INITIAL_UNLOCKED_GUN_COUNT) ? PlayerPrefs.GetInt("unlockedGunCount") : INITIAL_UNLOCKED_GUN_COUNT;
        }  

        // Limit starter health with unlocked gun counts if necessary
        int starterHealth;
        if(PlayerPrefs.GetInt("starterHealth") / GunData.GUN_UPGRADE_COUNT > unlockedGunCount)
        {
            starterHealth = unlockedGunCount * GunData.GUN_UPGRADE_COUNT;
        }
        else
        {
            starterHealth = (PlayerPrefs.GetInt("starterHealth") >= INITIAL_STARTER_HEALTH) ? PlayerPrefs.GetInt("starterHealth") : INITIAL_STARTER_HEALTH;
        }

        float damageMultiplier = (PlayerPrefs.GetFloat("damageMultiplier") >= INITIAL_DAMAGE_MULTIPLIER) ? PlayerPrefs.GetFloat("damageMultiplier") : INITIAL_DAMAGE_MULTIPLIER;
        Gun.SetDamageMultiplier(damageMultiplier);

        float rangeMultiplier = (PlayerPrefs.GetFloat("rangeMultiplier") >= INITIAL_RANGE_MULTIPLIER) ? PlayerPrefs.GetFloat("rangeMultiplier") : INITIAL_RANGE_MULTIPLIER;
        Gun.SetRangeMultiplier(rangeMultiplier);

        int moneyAmount = (PlayerPrefs.GetInt("moneyAmount") >= INITIAL_MONEY_AMOUNT) ? PlayerPrefs.GetInt("moneyAmount") : INITIAL_MONEY_AMOUNT;
        float moneyMultiplier = (PlayerPrefs.GetFloat("moneyMultiplier") >= INITIAL_MONEY_MULTIPLIER) ? PlayerPrefs.GetFloat("moneyMultiplier") : INITIAL_MONEY_MULTIPLIER;

        health = new Health(starterHealth);
        money = new Money(moneyAmount, moneyMultiplier);
    }

    public static void LoadLevelData(ref int levelIndex)
    {
        levelIndex = (PlayerPrefs.GetInt("levelIndex") >= INITIAL_LEVEL_INDEX) ? PlayerPrefs.GetInt("levelIndex") : INITIAL_LEVEL_INDEX;
    }

    public static void Save()
    {
        // Used for stopping save for debugging
        if(GameManager.instance.dontSave)
        {
            return;
        }

        PlayerPrefs.SetInt("unlockedGunCount", Player.instance.GetUnlockedGunCount());
        PlayerPrefs.SetInt("starterHealth", Player.instance.GetStarterHealth());

        PlayerPrefs.SetFloat("damageMultiplier", Gun.damageMultiplier);
        PlayerPrefs.SetFloat("rangeMultiplier", Gun.rangeMultiplier);
        
        PlayerPrefs.SetInt("moneyAmount", Player.instance.GetCurrentMoney());
        PlayerPrefs.SetFloat("moneyMultiplier", Player.instance.GetMoneyMultiplier());

        PlayerPrefs.SetInt("levelIndex", LevelManager.GetLevelIndex());
   }
}
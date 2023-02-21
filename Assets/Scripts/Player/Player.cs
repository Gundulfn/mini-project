using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    // Singleton
    public static Player instance { get; private set; }

    private PlayerController playerController;
    private Gun gun;

    private static int unlockedGunCount;
    private static int maxLevel;

    private static Health health;
    private static Money money;

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

        GameData.LoadPlayerData(ref health, ref money, ref unlockedGunCount);
        maxLevel = unlockedGunCount * GunData.GUN_UPGRADE_COUNT;
    }

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        gun = GetComponentInChildren<Gun>();

        GunUI.instance.SetActiveSlots(maxLevel);
        gun.SetLevel(health.current);

        UIManager.instance.UpdateMoneyText();
    }

    #region Health related functions

    public void SetHealthChange(int amount)
    {
        if (amount > 0)
        {
            AddHealth(amount);
        }
        else
        {
            amount *= -1;
            TakeDamage(amount);
        }
    }

    public void AddHealth(int amount)
    {
        int healthDifference = maxLevel - health.current;

        if (amount <= healthDifference)
        {
            health.Increase(amount);
        }
        else
        {
            health.Increase(healthDifference);

            AddMoney(amount - healthDifference);
        }

        gun.SetLevel(health.current);
    }

    public void TakeDamage(int damage)
    {
        health.Decrease(damage);

        if (health.current <= 0)
        {
            DestroyObject();
        }
        else
        {
            gun.SetLevel(health.current);
        }
    }

    public void DestroyObject()
    {
        playerController.enabled = false;
        gun.enabled = false;
        GunPartManager.instance.StopAnim();

        GameManager.instance.StopGame();

        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>())
        {
            childRenderer.enabled = false;
        }
    }

    public void IncreaseStarterHealth(int amount)
    {
        int healthDifference = maxLevel - health.current;

         if (amount <= healthDifference)
        {
            AddHealth(amount);
            health.IncreaseStarterHealth(amount);
        }
        else
        {
            AddHealth(healthDifference);
            health.IncreaseStarterHealth(healthDifference);
        }
        
    }

    public int GetStarterHealth()
    {
        return health.starterHealth;
    }

    #endregion

    #region Money functions

    public void AddMoney(int amount)
    {
        money.AddMoney(amount);
        UIManager.instance.UpdateMoneyText();
    }

    public void TakeMoney(int amount)
    {
        money.ReduceMoney(amount);
        UIManager.instance.UpdateMoneyText();
    }

    public int GetCurrentMoney()
    {
        return money.current;
    }

    public float GetMoneyMultiplier()
    {
        return money.moneyMultiplier;
    }

    public void IncreaseMoneyMultiplier(float amount)
    {
        money.IncreaseMoneyMultiplier(amount);
    }
    
    #endregion

    public int GetUnlockedGunCount()
    {
        return unlockedGunCount;
    }

    public void IncreaseUnlockedGunCount()
    {
        if (unlockedGunCount + 1 <= GunData.GetTotalGunCount())
        {
            unlockedGunCount++;
            UIManager.instance.UpdateNewGunUnlockedText((GunType)(unlockedGunCount - 1));
        }
    }

    //Max gun level and max health are same
    public int GetMaxLevel()
    {
        return maxLevel;
    }
}
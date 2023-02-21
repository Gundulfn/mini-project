public struct Money
{
    public float moneyMultiplier { get; private set;}
    public int current { get; private set;}

    public Money(int current = 0, float moneyMultiplier = 1)
    {
        this.current = current;
        this.moneyMultiplier = moneyMultiplier;
    }

    public void AddMoney(int amount)
    {
        current += (int)(amount * moneyMultiplier);
    }

    public bool ReduceMoney(int amount)
    {
        if (current >= amount)
        {
            current -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void IncreaseMoneyMultiplier(float amount)
    {
        moneyMultiplier += amount;
    }
}
public struct Health
{
    public int starterHealth {get;private set;}
    public int current { get; private set; }
    
    public Health(int current = 1)
    {
        starterHealth = current;
        this.current = starterHealth;
    }

    public void Increase(int amount)
    {
        current += amount;
    }

    public void Decrease(int amount)
    {
        current -= amount;

        if(current <= 0)
        {
            current = 0;
        }
    }

    public void IncreaseStarterHealth(int amount = 1)
    {
        starterHealth += amount;
    }
}
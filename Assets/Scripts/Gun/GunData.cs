public enum GunType
{
    None = -1, Pistol = 0, SMG = 1, Shotgun = 2
}

public struct GunData
{
    public const int GUN_UPGRADE_COUNT = 4;

    // GUN_DATA properties: (GunTypeIndex, Damage, Range, FireCooldown)
    private static readonly GunData[] GUN_DATAS = new GunData[]
    {
        new GunData(GunType.Pistol, 1, 8, .5f),
        new GunData(GunType.SMG, 2, 7, 0.3f),
        new GunData(GunType.Shotgun, 6, 5, .5f)
    };

    public GunType gunType { get; private set; }
    public int fireDamage { get; private set; }
    public float fireRange { get; private set; }
    public float fireSpeed { get; private set; }

    public GunData(GunType gunType, int fireDamage, float fireRange, float fireSpeed)
    {
        this.gunType = gunType;
        this.fireDamage = fireDamage;
        this.fireRange = fireRange;
        this.fireSpeed = fireSpeed;
    }

    public static void SetGunInfo(int gunTypeIndex, ref int fireDamage, ref float fireRange, ref float fireSpeed)
    {
        if (gunTypeIndex >= 0)
        {
            fireDamage = GUN_DATAS[gunTypeIndex].fireDamage;
            fireRange = GUN_DATAS[gunTypeIndex].fireRange;
            fireSpeed = GUN_DATAS[gunTypeIndex].fireSpeed;
        }
        else
        {
            fireDamage = 0;
            fireRange = 0;
            fireSpeed = 0;
        }
    }

    public static GunType GetGunType(int level)
    {
        if(level == GameData.INITIAL_STARTER_HEALTH)
        {
            return GunType.None;
        }
        else
        {
            return (GunType)( (level - 1) / GUN_UPGRADE_COUNT );
        }
    }

    public static int GetTotalGunCount()
    {
        return GUN_DATAS.Length;
    }
}
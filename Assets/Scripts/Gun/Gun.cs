using UnityEngine;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private LayerMask damageableObstacleLayerMask;

    [SerializeField]
    private GameObject bulletPrefab;

    //Object pooling
    private const int BULLET_POOL_COUNT = 20;
    private static Queue<Bullet> bullets = new Queue<Bullet>();
    private Bullet currentBullet;

    private RaycastHit hit;
    private bool readyToFire;
    private float fireCooldownTimer;

    private GunType currentGunType;

    private static int fireDamage;
    private static float fireRange, fireCooldown;

    public static float damageMultiplier { get; private set; }
    public static float rangeMultiplier { get; private set; }

    void Start()
    {
        if (bullets.Count <= 0)
        {
            for (int i = 0; i < BULLET_POOL_COUNT; i++)
            {
                currentBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
                currentBullet.Hide();

                bullets.Enqueue(currentBullet);
                DontDestroyOnLoad(currentBullet);
            }
        }
    }

    void Update()
    {
        // if gun is not built
        if (currentGunType == GunType.None)
        {
            return;
        }

        if (fireCooldownTimer > 0 && !readyToFire)
        {
            fireCooldownTimer -= Time.deltaTime;

            if (fireCooldownTimer <= 0)
            {
                readyToFire = true;
            }
        }

        if (readyToFire && Physics.Raycast(transform.position, transform.forward, out hit, GetFireRange(), damageableObstacleLayerMask.value) && hit.transform.GetComponent<IDamageable>() != null)
        {
            hit.transform.GetComponent<DamageableObstacle>().TakeDamage(Gun.GetFireDamage());
            
            //Bullet is used for fire effect
            currentBullet = bullets.Dequeue();
            currentBullet.transform.position = transform.position;
            currentBullet.StartMovement(hit.transform);

            fireCooldownTimer = fireCooldown;
            readyToFire = false;

            GunPartManager.instance.PlayFireAnim();
        }
    }

    #region  Gun Info

    public void SetLevel(int level)
    {
        currentGunType = GunData.GetGunType(level);

        GunData.SetGunInfo((int)currentGunType, ref fireDamage, ref fireRange, ref fireCooldown);

        if (level != 1)
        {
            GunUI.instance.SetLevel(level);
        }

        fireCooldownTimer = fireCooldown;
        GunPartManager.instance.SetHealth(level);
    }

    public static int GetFireDamage()
    {
        return (int)(fireDamage * damageMultiplier);
    }

    public static float GetFireRange()
    {
        return fireRange * rangeMultiplier;
    }

    #endregion

    #region  Multiplier Functions

    public static void SetDamageMultiplier(float value)
    {
        damageMultiplier = value;
    }

    public static void SetRangeMultiplier(float value)
    {
        rangeMultiplier = value;
    }

    public static void IncreaseDamageMultiplier(float multiplyingAmount)
    {
        damageMultiplier += multiplyingAmount;
    }

    public static void IncreaseRangeMultiplier(float multiplyingAmount)
    {
        rangeMultiplier += multiplyingAmount;
    }
    
    #endregion

    public static void EnqueueBullet(Bullet bullet)
    {
        bullets.Enqueue(bullet);
    }
}
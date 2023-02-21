using UnityEngine;

public class GunPartManager : MonoBehaviour
{
    // Singleton
    public static GunPartManager instance;
    private Animator animator;

    private GunPart[] gunParts;
    private int gunPartIndex;

    // Set from animation clips to update GunPart animators and renderers
    public bool updateGunParts;

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

        animator = GetComponent<Animator>();
        gunParts = GetComponentsInChildren<GunPart>();
    }

    void Update()
    {
        if (updateGunParts && animator.enabled)
        {
            for (gunPartIndex = 0; gunPartIndex < gunParts.Length; gunPartIndex++)
            {
                gunParts[gunPartIndex].UpdateCurrentState();
            }

            updateGunParts = false;
        }
    }

    public void PlayFireAnim()
    {
        animator.SetTrigger("fire");
    }

    // Trigger Gun display change animation
    public void SetHealth(int health)
    {
        animator.SetInteger("health", health);
    }

    public void StopAnim()
    {
        animator.enabled = false;
    }
}
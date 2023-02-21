using UnityEngine;

public class GunUI : MonoBehaviour
{
    public static GunUI instance { get; private set; }

    [SerializeField]
    private GameObject gunUISlotsObj;

    private RectTransform[] slots;

    private int currentSlotIndex;
    private Vector2 startPos, targetPos;

    private const float TIME_TO_REACH = 0.5f;
    private float timer;
    private float transitionSpeed;

    private const float BETWEEN_SLOT_DISTANCE = 150;

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

        slots = gunUISlotsObj.GetComponentsInChildren<RectTransform>(true);
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            // Increase speed for reaching target position in given time
            transitionSpeed += Time.deltaTime / TIME_TO_REACH;
            slots[0].anchoredPosition = Vector2.Lerp(startPos, targetPos, transitionSpeed);

            // Safe position setting for small differences from target position
            if (timer <= 0)
            {
                slots[0].anchoredPosition = targetPos;
            }
        }
    }

    public void SetLevel(int level)
    {
        if (level != currentSlotIndex && level < slots.Length)
        {
            if (timer > 0)
            {
                slots[0].anchoredPosition = targetPos;
                timer = 0;
            }
            
            startPos = slots[0].anchoredPosition;
            targetPos = startPos + Vector2.right * BETWEEN_SLOT_DISTANCE * (currentSlotIndex - level);

            transitionSpeed = 0;
            timer = TIME_TO_REACH;

            currentSlotIndex = level;
        }
    }

    public void SetActiveSlots(int maxLevel)
    {
        // i = 1 is for this game object's "RectTransfrom" component iterations
        // and maxLevel + 2 is for "GunType.Pistol"'s upgrade counts
        for (int i = 1; i < maxLevel + 2; i++)
        {
            slots[i].gameObject.SetActive(true);
        }
    }
}
using UnityEngine;

public class Cash : Interactable, IDroppable
{
    [SerializeField]
    private int moneyAmount = 1;

    [SerializeField]
    private DamageableObstacle dropfromDamageableObstacle;

    [SerializeField]
    private bool isDropInPlayer;

    void Start()
    {
        if (dropfromDamageableObstacle)
        {
            if (isDropInPlayer)
            {
                dropfromDamageableObstacle.SetCallback(DropIn);
            }
            else
            {
                dropfromDamageableObstacle.SetCallback(DropTo);
            }
        }

        destroyOnPlayerCollide = true;
    }

    public int GetMoneyAmount()
    {
        return moneyAmount;
    }

    // Drop behind of "dropfromDamageableObstacle" game object
    public void DropTo()
    {
        if (dropfromDamageableObstacle)
        {
            StartCoroutine(IDroppable.DropThroughTime(transform));
        }
    }

    // Drop on player
    public void DropIn()
    {
        if (dropfromDamageableObstacle)
        {
            StartCoroutine(IDroppable.DropThroughTime(Player.instance.transform, transform));
        }
    }
}
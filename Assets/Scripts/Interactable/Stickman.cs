using UnityEngine;

public class Stickman : Interactable, IDroppable
{
    [SerializeField]
    private int stickmanCount = 1;

    public int StickmanCount { get{return stickmanCount;}}

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

    public void DropTo()
    {
        if (dropfromDamageableObstacle)
        {
            StartCoroutine(IDroppable.DropThroughTime(transform));
        }
    }

    public void DropIn()
    {
        if (dropfromDamageableObstacle)
        {
            StartCoroutine(IDroppable.DropThroughTime(Player.instance.transform, transform));
        }
    }
}
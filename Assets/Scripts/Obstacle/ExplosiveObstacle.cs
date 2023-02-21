using UnityEngine;

public class ExplosiveObstacle : DamageableObstacle
{
    private const float EXPLOSION_RADIUS = 1;

    public override void DestroyObject()
    {
        DamageableObstacle[] damageables = FindObjectsOfType<DamageableObstacle>();
        
        for (int i = 0; i < damageables.Length; i++)
        {
            if (damageables[i] != this && Vector3.Distance(transform.position, damageables[i].transform.position) <= EXPLOSION_RADIUS)
            {
                damageables[i].DestroyObject();
            }
        }

        base.DestroyObject();
    }
}
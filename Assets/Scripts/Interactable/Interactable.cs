using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    internal bool destroyOnPlayerCollide = false;

    public virtual void Interact(object obj)
    {
        if (obj is PlayerCollider && destroyOnPlayerCollide)
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            Interactable interactable = other.GetComponent<Interactable>();

            // Invoking this here for optimizing physic calculations by reducing rigidbody count in game
            interactable.Interact(this);

            if (interactable is Obstacle)
            {
                Player.instance.TakeDamage( (interactable as Obstacle).GetHitDamage() );
            }
            else if (interactable is Stickman)
            {
                Player.instance.AddHealth( (interactable as Stickman).StickmanCount );
            }
            else if (interactable is Gate)
            {
                Player.instance.SetHealthChange( (interactable as Gate).GetGateValue() );
            }
            else if (interactable is Cash)
            {
                Player.instance.AddMoney( (interactable as Cash).GetMoneyAmount() );
            }
        }
        else if(other.CompareTag(GameManager.FINISH_LINE_TAG))
        {
            GameManager.instance.SetNextLevel();
        }
    }
}
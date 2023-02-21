using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float BULLET_SPEED = 75;

    private Transform target;
    private TrailRenderer trailRenderer;
    private Vector3 moveDirection;
    
    private const float DISTANCE_LIMIT = .3f;
    private float distance;

    void Update()
    {
        if (target)
        {
            transform.Translate(moveDirection * Time.deltaTime * BULLET_SPEED);
            
            distance = target.position.z - transform.position.z;
            
            if(distance < 0)
            {
                distance *= -1; 
            }

            if(distance <= DISTANCE_LIMIT || distance > Gun.GetFireRange())
            {
                Hide();
                Gun.EnqueueBullet(this);
                target = null;
            }
        }
    }

    public void StartMovement(Transform target)
    {
        this.target = target;
        moveDirection = (target.position - transform.position).normalized;
        
        trailRenderer.enabled = true;
    }

    public void Hide()
    {
        if (!trailRenderer)
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }

        trailRenderer.enabled = false;
    }
}
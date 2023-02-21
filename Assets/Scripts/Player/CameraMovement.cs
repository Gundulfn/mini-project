using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private static Vector3 offset;

    private const float DAMPING_SPEED = 10;
    private Vector3 targetPos;

    void Awake()
    {
        if (target)
        {
            offset = target.position - transform.position;
        }
    }

    void Update()
    {
        if (target)
        {
            targetPos = target.position - offset ;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * DAMPING_SPEED);
        }
    }
}
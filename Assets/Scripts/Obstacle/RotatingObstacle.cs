using UnityEngine;

public class RotatingObstacle : Obstacle
{
    private const float ROTATION_SPEED = 50;

    void Update()
    {
        if(Time.timeScale == 1)
        {
            transform.Rotate(0, ROTATION_SPEED * Time.deltaTime, 0);
        }
    }
}
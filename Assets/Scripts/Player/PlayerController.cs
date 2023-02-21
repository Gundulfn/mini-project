using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private const float X_LIMIT = 1.5f;
    private const float TOUCH_POS_NORMALIZER = .01f;
    private const float MOVE_SPEED = 1.5f;

    private Touch touch;
    private float playerPosDirection;
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            // If player did not touch any UI element and player's finger is moving
            if (touch.phase == TouchPhase.Moved && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                if (GameManager.instance.IsGameWaitingToStart())
                {
                    GameManager.instance.StartGame();
                    return;
                }

                // Check horizontal swipe amount and set player position between limits (X_LIMIT) on either direction
                if (transform.position.x + touch.deltaPosition.x * TOUCH_POS_NORMALIZER < X_LIMIT && transform.position.x + touch.deltaPosition.x * TOUCH_POS_NORMALIZER > -X_LIMIT)
                {
                    transform.position += touch.deltaPosition.x * TOUCH_POS_NORMALIZER * Vector3.right;
                }
                else
                {
                    playerPosDirection = (transform.position.x > 0) ? 1 : -1;
                    transform.position = new Vector3(X_LIMIT * playerPosDirection, transform.position.y, transform.position.z);
                }
            }
        }

        transform.position += Vector3.forward * Time.deltaTime * MOVE_SPEED;
    }
}
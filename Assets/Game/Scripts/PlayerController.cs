using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed, xLimit, zLimit, yConst;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 TouchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-TouchDeltaPosition.x * Speed, -TouchDeltaPosition.y * Speed, 0);

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -xLimit, xLimit),
                Mathf.Clamp(transform.position.y, yConst, yConst),
                Mathf.Clamp(transform.position.z, -zLimit, zLimit)
            );
        }
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // The player to follow
    public Vector3 offset;          //  offset (e.g., x = 2 to look slightly ahead)
    public float smoothSpeed = 0.125f; // Camera smoothing factor

    private float fixedY;           // Y position to lock camera at

    void Start()
    {
        // Store the initial Y position so we can lock it
        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Only follow the target on the X axis
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, fixedY, -10f);
            Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothed;
        }
    }
}

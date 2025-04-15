using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;             // The player to follow
    public Vector3 offset;               
    public float smoothSpeed = 0.125f;   

    public float leftLimit;
    public float rightLimit;
    public float bottomLimit;
    public float topLimit;

    void LateUpdate()
    {
        if (player == null) return;

        // Target position based on player + offset
        Vector3 targetPosition = player.position + offset;

        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        
        float clampedX = Mathf.Clamp(smoothedPosition.x, leftLimit, rightLimit);
        float clampedY = Mathf.Clamp(smoothedPosition.y, bottomLimit, topLimit);

        
        transform.position = new Vector3(clampedX, clampedY, -10f);
    }
}

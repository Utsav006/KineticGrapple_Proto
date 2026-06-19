using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    [Header("Camera Positioning")]
    [Tooltip("Pushes the camera ahead so the player sits on the left side of the screen")]
    public float xOffset = 6f;

    [Tooltip("Locks the camera height so the screen does not bounce violently while swinging")]
    public float lockedY = 2f;

    // LateUpdate runs after all physics calculations are finished, preventing visual jitter
    void LateUpdate()
    {
        if (player != null)
        {
            // Track the player's X position, lock the Y height, and keep the standard -10 Z depth for 2D cameras
            transform.position = new Vector3(player.position.x + xOffset, lockedY, -10f);
        }
    }
}
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public Transform player;
    public float yOffset = -15f;

    void Update()
    {
        if (player != null)
        {
            // Only update the X position to match the player. Keep Y locked to the offset.
            transform.position = new Vector3(player.position.x, yOffset, 0f);
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object passing through the sensor is the player
        if (collision.gameObject.name == "Player")
        {
            GameManager.Instance.RestartLevel();
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GoalZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object reaching the goal is the player
        if (collision.gameObject.name == "Player")
        {
            GameManager.Instance.WinLevel();
        }
    }
}
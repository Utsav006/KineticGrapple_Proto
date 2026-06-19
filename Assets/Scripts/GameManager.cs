using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RestartLevel()
    {
        // Ensure time is running normally before reloading
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinLevel()
    {
        Debug.Log("Milestone Reached: Level Complete!");
        // Freeze the physics simulation to visually confirm the win
        Time.timeScale = 0f;
    }
}
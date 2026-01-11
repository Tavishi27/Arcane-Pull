using UnityEngine;
using UnityEngine.SceneManagement;

// PauseMenuBehavior controls the pause menu and pausing mechanics
public class PauseMenuBehavior : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public AudioClip buttonClickSFX;

    bool gameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused)
            {
                // pause game
                PauseGame();
            }
            else
            {
                // resume game
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        PlayButtonClickSound();

        gameIsPaused = true;
        Time.timeScale = 0f;
        if (pauseMenuPanel)
        {
            pauseMenuPanel.SetActive(true);
        }

        // Enable cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        if (pauseMenuPanel)
        {
            pauseMenuPanel.SetActive(false);
        }

        // Disable cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayButtonClickSound();
    }

    // LoadMainMenu loads the main menu (incomplete)
    public void LoadMainMenu()
    {
        // Debug.Log("Loading main menu...");

        Time.timeScale = 1f;
        PlayButtonClickSound();

        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        PlayButtonClickSound();

        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    void PlayButtonClickSound()
    {
        if (buttonClickSFX)
        {
            AudioSource.PlayClipAtPoint(buttonClickSFX, Camera.main.transform.position);
        }
    }
}

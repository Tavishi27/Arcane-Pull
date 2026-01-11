using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerDataManager))]

public class MainMenuBehavior : MonoBehaviour
{

    public GameObject mainMenu;
    public AudioClip buttonClickSFX;

    SettingsManager settingsManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsManager = GameObject.FindAnyObjectByType<SettingsManager>();
    }

    public void StartGame()
    {
        PlayButtonClickSound();

        SceneManager.LoadScene(1);
    }

/*    public void OpenHowToPlayWindow()
    {
        PlayButtonClickSound();

        if (howToPlayWindow && mainMenu)
            OpenWindow(howToPlayWindow);
    }

    public void CloseHowToPlayWindow()
    {
        PlayButtonClickSound();

        if (howToPlayWindow && mainMenu)
            CloseWindow(howToPlayWindow);
    }

    public void OpenPlayerInfoWindow()
    {
        PlayButtonClickSound();

        if (playerInfoWindow && mainMenu)
            OpenWindow(playerInfoWindow);
    }

    public void ClosePlayerInfoWindow()
    {
        PlayButtonClickSound();

        if (playerInfoWindow && mainMenu)
            CloseWindow(playerInfoWindow);
    }*/

    public void QuitGame()
    {
        PlayButtonClickSound();

        Debug.Log("Exiting the game...");
        Application.Quit();
    }

    void PlayButtonClickSound()
    {
        if (buttonClickSFX)
        {
            AudioSource.PlayClipAtPoint(buttonClickSFX, Camera.main.transform.position);
        }
    }

    public void OpenWindow(GameObject window)
    {
        PlayButtonClickSound();

        mainMenu.SetActive(false);
        window.SetActive(true);
    }

    public void CloseWindow(GameObject window)
    {
        PlayButtonClickSound();

        mainMenu.SetActive(true);
        window.SetActive(false);
    }

    public void OpenAndLoadSettingsWindow(GameObject window)
    {
        OpenWindow(window);

        if (settingsManager != null)
        {
            settingsManager.LoadSettings();
        }
    }

    public void CloseAndUpdateSettingsWindow(GameObject window)
    {
        if (settingsManager != null)
        {
            settingsManager.SaveSettings();
        }

        CloseWindow(window);
    }

    public void ResetPlayerData()
    {
        PlayButtonClickSound();

        PlayerDataManager playerDataManager = GetComponent<PlayerDataManager>();
        playerDataManager.WipeAllData();
    }
}

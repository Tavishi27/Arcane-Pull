using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static bool IsPlaying { get; private set; }

    public int levelNumber = 1;
    public float levelTime = 30f;
    public string nextLevel;
    public TMP_Text timerText;
    public TMP_Text messageText;
    public GameObject nextButton;
    public TMP_Text buttonLabel;
    public Button buttonComponent;

    private float countDown;
    public static LevelManager Instance;

    public float groundLevel = 0.0f;
    private Transform playerTransform;
    public GameObject ground;

    public AudioClip outOfTimeSFX;
    public AudioClip buttonClickSFX;

    public bool cursorIsHidden = true;

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

        playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

    void Start()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
        if (difficulty.Equals("Hard"))
        {
            levelTime = levelTime / 2;
        }

        countDown = levelTime;
        IsPlaying = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (messageText) messageText.enabled = false;
        if (nextButton) nextButton.SetActive(false);
    }

    void Update()
    {
        if (IsPlaying)
        {
            float totalTime = PlayerPrefs.GetFloat("Total Time Spent") + Time.deltaTime;
            PlayerPrefs.SetFloat("Total Time Spent", totalTime);
            PlayerPrefs.Save();

            if (playerTransform == null)
            {
                LevelLost();
                return;
            }



            if (levelTime > 0)
            {
                countDown -= Time.deltaTime;

                if (countDown <= 0)
                {
                    if (outOfTimeSFX)
                    {
                        AudioSource.PlayClipAtPoint(outOfTimeSFX, Camera.main.transform.position);
                    }
                    
                    countDown = 0;
                    LevelLost();
                }

                SetTimerText();
            }

            if (playerTransform.position.y <= groundLevel + 5)
            {
                LevelLost();
            }

            MoveGround();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (cursorIsHidden)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                cursorIsHidden = false;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                cursorIsHidden = true;
            }
        }
    }

    void MoveGround()
    {
        if (ground)
        {
            ground.transform.position = new Vector3(playerTransform.position.x, groundLevel, ground.transform.position.z);
        }
    }

    void SetTimerText()
    {
        if (timerText)
            timerText.text = "Time: " + countDown.ToString("0.00");
    }

    public void LevelBeat()
    {
        IsPlaying = false;
        ShowMessage("You Win!");
        ShowButton("Next Level", LoadNextLevel);
        UpdatePlayerData();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }

    public void LevelLost()
    {
        IsPlaying = false;
        ShowMessage("You Lose");
        ShowButton("Try Again", RestartLevel);
        
        int totalDeaths = PlayerPrefs.GetInt("Total Deaths") + 1;
        PlayerPrefs.SetInt("Total Deaths", totalDeaths);
        PlayerPrefs.Save();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ShowMessage(string msg)
    {
        if (messageText)
        {
            messageText.enabled = true;
            messageText.text = msg;
        }
    }

    void ShowButton(string label, UnityEngine.Events.UnityAction action)
    {
        if (nextButton && buttonLabel && buttonComponent)
        {
            nextButton.SetActive(true);
            buttonLabel.text = label;

            buttonComponent.onClick.RemoveAllListeners();
            buttonComponent.onClick.AddListener(action);
        }
    }

    public void LoadNextLevel()
    {
        PlayButtonClickSound(); // LoadNextLevel is only called when the next level button is clicked

        if (!string.IsNullOrEmpty(nextLevel))
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.LogWarning("Next level not assigned");
        }
    }

    public void RestartLevel()
    {
        PlayButtonClickSound(); // RestartLevel is only called when the try again button is clicked

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

    void UpdatePlayerData()
    {
        string str = "Level " + levelNumber + " Time";
        float prevLevelTime = PlayerPrefs.GetFloat(str, Mathf.Infinity);
        float currLevelTime = Time.timeSinceLevelLoad;
        if (currLevelTime < prevLevelTime)
        {
            PlayerPrefs.SetFloat(str, currLevelTime);
        }

        int highestLevelCompleted = PlayerPrefs.GetInt("Highest Level Completed", 0);
        if (levelNumber > highestLevelCompleted)
        {
            PlayerPrefs.SetInt("Highest Level Completed", levelNumber);
        }

        PlayerPrefs.Save();
    }
}

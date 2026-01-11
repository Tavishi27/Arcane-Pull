using UnityEngine;
using TMPro;

public class PlayerDataManager : MonoBehaviour
{
    public TMP_Text highestLevelText;
    public TMP_Text totalTimeText;
    public TMP_Text totalDeathsText;
    public TMP_Text levelOneText;
    public TMP_Text levelTwoText;
    public TMP_Text levelThreeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdatePlayerInfo();
    }

    public void WipeAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        UpdatePlayerInfo();
    }

    void UpdatePlayerInfo()
    {
        string text = "";

        // highest level
        int highestLevel = PlayerPrefs.GetInt("Highest Level Completed", -1);
        if (highestLevel >  0) {
            text = "Highest level completed: " + highestLevel;
        } else
        {
            text = "Highest level completed: N/A";
        }
        if (highestLevelText)
        {
            highestLevelText.text = text;
        }

        // total time
        float totalTime = PlayerPrefs.GetFloat("Total Time Spent", -1.0f);
        if (totalTime > 0)
        {
            text = "Total time spent: " + totalTime.ToString("0.00") + " sec";
        } else {
            text = "Total time spent: N/A";
        }
        if (totalTimeText)
        {
            totalTimeText.text = text;
        }

        // total deaths
        int totalDeaths = PlayerPrefs.GetInt("Total Deaths", 0);
        text = "Number of deaths: " + totalDeaths;
        if (totalDeathsText)
        {
            totalDeathsText.text = text;
        }

        // level one
        float levelOneTime = PlayerPrefs.GetFloat("Level 1 Time", -1.0f);
        if (levelOneTime > 0)
        {
            text = "Level 1 completion time: " + levelOneTime.ToString("0.00") + " sec";
        } else
        {
            text = "Level 1 completion time: N/A";
        }
        if (levelOneText)
        {
            levelOneText.text = text;
        }

        // level two
        float levelTwoTime = PlayerPrefs.GetFloat("Level 2 Time", -1.0f);
        if (levelTwoTime > 0)
        {
            text = "Level 2 completion time: " + levelTwoTime.ToString("0.00") + " sec";
        }
        else
        {
            text = "Level 2 completion time: N/A";
        }
        if (levelTwoText)
        {
            levelTwoText.text = text;
        }

        // level three
        float levelThreeTime = PlayerPrefs.GetFloat("Level 3 Time", -1.0f);
        if (levelThreeTime > 0)
        {
            text = "Level 3 completion time: " + levelThreeTime.ToString("0.00") + " sec";
        }
        else
        {
            text = "Level 3 completion time: N/A";
        }
        if (levelThreeText)
        {
            levelThreeText.text = text;
        }
    }
}

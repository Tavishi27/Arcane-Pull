using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public Slider mouseSensitivitySlider;
    public TMP_Dropdown difficultyDropdown;
    public Toggle hintsToggle;

    public void LoadSettings()
    {
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        mouseSensitivitySlider.value = savedSensitivity;

        string savedDifficulty = PlayerPrefs.GetString("Difficulty", "Normal");
        difficultyDropdown.value = 0;

        bool hintsOn = PlayerPrefs.GetInt("Hints", 0) == 1;
        hintsToggle.isOn = hintsOn;
    }

    public void SaveSettings()
    {
        float sensitivity = mouseSensitivitySlider.value;
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
        
        string difficulty;
        if (difficultyDropdown.value == 0)
        {
            difficulty = "Normal";
        } else
        {
            difficulty = "Hard";
        }
        PlayerPrefs.SetString("Difficulty", difficulty);

        bool hints = hintsToggle.isOn;
        PlayerPrefs.SetInt("Hints", hints ? 1 : 0);

        PlayerPrefs.Save();
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class IconBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hint;

    bool hintsAlwaysOn;

    void Start()
    {
        hintsAlwaysOn = PlayerPrefs.GetInt("Hints", 0) == 1;

        if (hintsAlwaysOn)
        {
            hint.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("Mouse entered the UI image");
        if (hint) 
            hint.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("Mouse exited the UI image");
        if (hint && !hintsAlwaysOn) 
            hint.SetActive(false);
    }
}
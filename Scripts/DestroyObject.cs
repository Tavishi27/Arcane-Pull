using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float duration = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, duration);
    }
}

// using UnityEditor.Rendering;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    public float xSpeed = 400.0f;
    public float ySpeed = 200.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    private float x = 0.0f;
    private float y = 0.0f;
    private float z = 0.0f; // will become 180.0f when camera is flipped

    private int gDir = 1;

    private float mouseSensitivity;

    void Start()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (!LevelManager.IsPlaying) return;

        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime * gDir * mouseSensitivity;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime * gDir * mouseSensitivity;
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, z);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public void Flip()
    {
        if (Physics.gravity.y < 0.0f)
        {
            z = 0.0f;
        }
        else
        {
            z = 180.0f;
        }
        gDir *= -1;

        float temp = yMaxLimit * -1;
        yMaxLimit = yMinLimit * -1;
        yMinLimit = temp;
    }
}

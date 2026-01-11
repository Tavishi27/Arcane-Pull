using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerPlatformSkill))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerGravitySkill : MonoBehaviour
{
    public bool gravitySkillisEnabled = true;
    public KeyCode gravityKey = KeyCode.Q;

    private bool isCooldown = false;
    public float cooldownTime = 2.0f;

    public int maxNumUses = 2;
    private int numUsesLeft;
    public TMP_Text gravitySkillText;

    public GameObject gravitySkillEffectPrefab;

    private PlayerPlatformSkill playerPlatformSkill;
    private CameraController cameraController;
    private PlayerController playerController;

    [Header("Audio")]
    public AudioClip gravityFlipSound;
    private AudioSource audioSource;

    void Start()
    {
        playerPlatformSkill = GetComponent<PlayerPlatformSkill>();
        cameraController = FindAnyObjectByType<CameraController>();
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();

        Physics.gravity = new Vector3(0, -9.81f, 0);
        numUsesLeft = maxNumUses;
        UpdateGravitySkillText();
    }

    void Update()
    {
        if (numUsesLeft > 0 && gravitySkillisEnabled && !isCooldown && Input.GetKeyDown(gravityKey))
        {
            if (gravitySkillEffectPrefab)
            {
                Instantiate(gravitySkillEffectPrefab, transform.position, transform.rotation);
            }

            Flip();
            numUsesLeft--;
            UpdateGravitySkillText();
        }
    }

    void Flip()
    {
        Physics.gravity *= -1;
        playerPlatformSkill.upDirection *= -1;

        if (cameraController)
        {
            cameraController.Flip();
        }

        FlipPlayer();
        FlipEnemies();

        // Play the SoundSFX
        if (gravityFlipSound && audioSource)
        {
            audioSource.PlayOneShot(gravityFlipSound);
        }

        isCooldown = true;
        StartCoroutine(Cooldown());
    }

    void FlipPlayer()
    {
        playerController.isInAir = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;

        StartCoroutine(SmoothFlip());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

    IEnumerator SmoothFlip(float duration = 0.25f)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }

    void FlipEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("NormalEnemy");
        foreach (GameObject enemy in enemies)
        {
            NormalEnemyBehavior normalEnemyBehavior = enemy.GetComponent<NormalEnemyBehavior>();

            if (normalEnemyBehavior != null)
            {
                normalEnemyBehavior.Flip();
            }
        }
    }

    void UpdateGravitySkillText()
    {
        if (gravitySkillText != null)
        {
            gravitySkillText.text = numUsesLeft.ToString();
        }
    }
}

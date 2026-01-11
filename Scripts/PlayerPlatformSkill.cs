using TMPro;
using UnityEngine;

public class PlayerPlatformSkill : MonoBehaviour
{
    public GameObject platformPrefab;
    public float checkDistance = 1.0f;
    public float platformYOffset = -1.0f;
    public float platformLifetime = 5.0f;

    private bool isCooldown = false;
    public float cooldownTime = 2.0f;

    public int upDirection = 1; // reverses when gravity is reversed

    public GameObject buildEffectPrefab;

    public int maxNumUses = 5;
    private int numUsesLeft;
    public TMP_Text platformSkillText;

    [Header("Audio")]
    public AudioClip spawnPlatformSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        numUsesLeft = maxNumUses;
        UpdatePlatformSkillText();
    }

    void Update()
    {
        if (numUsesLeft > 0 && Input.GetKeyDown(KeyCode.E) && !isCooldown)
        {
            if (!IsGrounded())
            {
                SpawnPlatform();
                numUsesLeft--;
                UpdatePlatformSkillText();

                StartCoroutine(StartCooldown());
            }
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down * upDirection, checkDistance);
    }

    void SpawnPlatform()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + (platformYOffset * upDirection), transform.position.z);
        GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        Destroy(platform, platformLifetime);

        // Play SoundSFX
        if (spawnPlatformSound && audioSource)
        {
            audioSource.PlayOneShot(spawnPlatformSound);
        }
        
        // Play VFX
        if (buildEffectPrefab)
        {
            Instantiate(buildEffectPrefab, spawnPosition, Quaternion.identity);
        }
    }

    System.Collections.IEnumerator StartCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

    void UpdatePlatformSkillText()
    {
        if (platformSkillText != null)
        {
            platformSkillText.text = numUsesLeft.ToString();
        }
    }
}

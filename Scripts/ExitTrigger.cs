using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public GameObject teleportEffectPrefab;
    public AudioClip teleportClip;

    public int levelNumber = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && LevelManager.IsPlaying)
        {
            GameObject player = other.gameObject;

            LevelManager.Instance.LevelBeat();
            
            if (teleportEffectPrefab)
            {
                Instantiate(teleportEffectPrefab, player.transform.position, player.transform.rotation);
                Destroy(player, 0.5f);

                if (teleportClip != null)
                {
                    AudioSource.PlayClipAtPoint(teleportClip, Camera.main.transform.position);
                }
            }
        }
    }
}

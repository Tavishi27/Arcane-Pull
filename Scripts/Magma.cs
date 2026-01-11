using System.Drawing;
using UnityEngine;

public class Magma : MonoBehaviour
{
    public float damageAmount = 10f;      public float damageInterval = 1f; 

    private float damageTimer = 0f;

    public AudioClip lavaDamageSFX;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                }

                if (lavaDamageSFX != null)
                {
                    AudioSource.PlayClipAtPoint(lavaDamageSFX, Camera.main.transform.position);
                }

                damageTimer = 0f; 
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer = 0f;
        }
    }
}

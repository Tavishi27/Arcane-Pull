using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{    
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject hitEffectPrefab;

    public Slider healthSlider;

    void Start()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty", "Normal");
        if (difficulty.Equals("Hard"))
        {
            maxHealth = maxHealth / 3;
        }

        currentHealth = maxHealth;
        UpdateHealthSlider();
    }
    public void TakeDamage(float amount)
    {
        if (hitEffectPrefab)
        {
            Instantiate(hitEffectPrefab, transform.position, transform.rotation);
        }

        currentHealth -= amount;
        UpdateHealthSlider();

        if (currentHealth < 0f)
        {
            currentHealth = 0f;
        }

        if (currentHealth == 0f)
        {
            Die();
        }
        else
        {
            Debug.Log("Player get " + amount + " damage, remain health " + currentHealth);
        }
    }


    private void Die()
    {

        Debug.Log("Player die");

        Destroy(gameObject);
    }

    // UpdateHealthSlider updates the UI health bar
    void UpdateHealthSlider()
    {
        if (healthSlider)
        {
            healthSlider.value = currentHealth;
        }
    }
}

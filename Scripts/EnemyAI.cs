using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab; 
    public Transform firePoint;  
    public float fireRate = 1f;  
    public float projectileSpeed = 20f;   
    private float fireCooldown = 0f; 

    void Start()
    {

    }

    void Update()
    {
        if (fireCooldown <= 0f)
        {
            Shoot(); 
            fireCooldown = 1f / fireRate;
        }

        fireCooldown -= Time.deltaTime;
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            return;
        }

        Vector3 shootDir = transform.forward;

        GameObject proj = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.LookRotation(shootDir)
        );

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootDir * projectileSpeed;
        }
    }
}

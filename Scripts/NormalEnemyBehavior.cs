using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NormalEnemyBehavior : MonoBehaviour
{
    public Transform target;
    public float damageAmount = 100f;
    public AudioClip enemySFX;
    public AudioClip hitSFX;
    public bool isAnimated = false;
    public float detectionRange = 10f;

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    private int patrolIndex = 0;
    public float waitTime = 2f;
    private float waitTimer = 0f;
    private bool waiting = false;

    private NavMeshAgent agent;
    private Animator animator;

    private enum State { Patrol, Chase }
    private State currentState = State.Patrol;

    // Flip 相关
    private bool isFlipped = false;
    private bool isFlipping = false;
    public float flipDuration = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (!target)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) target = player.transform;
        }

        if (isAnimated)
        {
            animator = GetComponent<Animator>();
        }

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[patrolIndex].position);
        }
    }

    void Update()
    {
        if (!LevelManager.IsPlaying) return;

        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Chase;
            agent.SetDestination(target.position);
        }
        else
        {
            currentState = State.Patrol;
            Patrol();
        }

        if (isAnimated)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (!waiting)
            {
                waiting = true;
                waitTimer = 0f;
            }

            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                waiting = false;
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[patrolIndex].position);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            if (hitSFX)
            {
                AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position);
            }
        }
    }

    public void DestroyEnemy()
    {
        if (enemySFX)
        {
            AudioSource.PlayClipAtPoint(enemySFX, Camera.main.transform.position);
        }

        if (isAnimated && animator)
        {
            animator.SetTrigger("OnCollisionEnter");
        }

        Destroy(gameObject, 1f);
    }

    // ✅ 补回 Flip 功能
    public void Flip()
    {
        if (!isFlipping)
        {
            isFlipped = !isFlipped;
            StartCoroutine(FlipCoroutine());
        }
    }

    private IEnumerator FlipCoroutine()
    {
        isFlipping = true;

        Quaternion startRotation = transform.rotation;
        float targetX = isFlipped ? 180f : 0f;
        Quaternion endRotation = Quaternion.Euler(targetX, startRotation.eulerAngles.y + 180f, startRotation.eulerAngles.z);

        float elapsed = 0f;
        while (elapsed < flipDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / flipDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        isFlipping = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public Transform cameraTransform;

    public AudioClip jumpSound;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private int jumpCount = 0;
    private int maxJumps = 2;

    private AudioSource audioSource;

    private Animator animator;
    private int animState;
    public bool isInAir = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = (forward * v + right * h).normalized;

        Vector3 currentVelocity = rb.linearVelocity;
        rb.linearVelocity = new Vector3(moveDir.x * speed, currentVelocity.y, moveDir.z * speed);


        if (moveDir != Vector3.zero)
        {
            //transform.forward = moveDir;

            Vector3 gravityUp = -Physics.gravity.normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir, gravityUp), Time.deltaTime * rotationSpeed);
        }

        // animate player
        Vector3 inputVector = new Vector3(h, 0, v);
        inputVector.Normalize();
        if (isInAir)
        {
            animState = 4;
        }
        else if (inputVector.magnitude >= 0.01f)
        {
            animState = 1;
        }
        else
        {
            animState = 0;
        }

        if (animator)
        {
            animator.SetInteger("animState", animState);
        }
    }

    void Jump()
    {
        float upDir = Physics.gravity.normalized.y * -1;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce * upDir, ForceMode.Impulse);
        jumpCount++;
        PlayJumpSound();

        isInAir = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            float upDir = Physics.gravity.normalized.y * -1;

            if (contact.normal.y * upDir > 0.5f)
            {
                jumpCount = 0;
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                isInAir = false;

                break;
            }
        }
    }

    void Update()
    {
        if (!LevelManager.IsPlaying) return;
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
        }
    }


    void PlayJumpSound()
    {
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }
}

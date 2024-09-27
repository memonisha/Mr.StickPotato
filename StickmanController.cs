

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float jumpForce = 10f; // Jump force
    public LayerMask groundLayer; // Layer for ground detection

    private Rigidbody2D rb;
    private bool isGrounded;

    public Transform groundCheck; // A point to check for ground collision
    public float groundCheckRadius = 0.2f; // Radius of ground check area

    // Limb references
    public Transform leftArm;
    public Transform rightArm;
    public Transform leftLeg;
    public Transform rightLeg;

    // Sound references
    public AudioClip limbMoveSound;
    public AudioClip jumpSound; // New jump sound
    private AudioSource audioSource;

    // Rotation limits for limbs
    private float leftArmRotation = 0f;
    private float rightArmRotation = 0f;
    private float leftLegRotation = 0f;
    private float rightLegRotation = 0f;

    // Rotation limits for how far limbs can rotate
    public float limbRotationLimit = 20f; // Maximum rotation angle in degrees

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Access Rigidbody2D component
        audioSource = GetComponent<AudioSource>(); // Access AudioSource component
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Ground check: Cast a small circle below the stickman to check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jumping logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply jump force
            PlayJumpSound(); // Play jump sound when spacebar is pressed
        }

        // Move limbs with key presses
        MoveLimbs();
    }

    // Function to move limbs based on input
    void MoveLimbs()
    {
        // Left Arm (Q for up, W for down)
        if (Input.GetKey(KeyCode.Q))
        {
            leftArmRotation += 2f; // Increase rotation
        }
        if (Input.GetKey(KeyCode.W))
        {
            leftArmRotation -= 2f; // Decrease rotation
        }
        leftArmRotation = Mathf.Clamp(leftArmRotation, -limbRotationLimit, limbRotationLimit);
        leftArm.localRotation = Quaternion.Euler(0, 0, leftArmRotation); // Apply clamped rotation
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W))
        {
            PlayLimbSound();
        }

        // Right Arm (O for up, P for down)
        if (Input.GetKey(KeyCode.O))
        {
            rightArmRotation += 2f; // Increase rotation
        }
        if (Input.GetKey(KeyCode.P))
        {
            rightArmRotation -= 2f; // Decrease rotation
        }
        rightArmRotation = Mathf.Clamp(rightArmRotation, -limbRotationLimit, limbRotationLimit);
        rightArm.localRotation = Quaternion.Euler(0, 0, rightArmRotation); // Apply clamped rotation
        if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.P))
        {
            PlayLimbSound();
        }

        // Left Leg (Z for up, X for down)
        if (Input.GetKey(KeyCode.Z))
        {
            leftLegRotation += 2f; // Increase rotation
        }
        if (Input.GetKey(KeyCode.X))
        {
            leftLegRotation -= 2f; // Decrease rotation
        }
        leftLegRotation = Mathf.Clamp(leftLegRotation, -limbRotationLimit, limbRotationLimit);
        leftLeg.localRotation = Quaternion.Euler(0, 0, leftLegRotation); // Apply clamped rotation
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            PlayLimbSound();
        }

        // Right Leg (N for up, M for down)
        if (Input.GetKey(KeyCode.N))
        {
            rightLegRotation += 2f; // Increase rotation
        }
        if (Input.GetKey(KeyCode.M))
        {
            rightLegRotation -= 2f; // Decrease rotation
        }
        rightLegRotation = Mathf.Clamp(rightLegRotation, -limbRotationLimit, limbRotationLimit);
        rightLeg.localRotation = Quaternion.Euler(0, 0, rightLegRotation); // Apply clamped rotation
        if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M))
        {
            PlayLimbSound();
        }
    }

    // Function to play sound when limbs move
    void PlayLimbSound()
    {
        audioSource.PlayOneShot(limbMoveSound); // Play limb movement sound
    }

    // Function to play sound when jumping
    void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound); // Play jump sound
    }
}

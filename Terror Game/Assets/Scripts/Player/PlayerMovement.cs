using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject EffectSound;
    public Animator anim;

    [Header("Propiedades")]
    public float speed = 6f;
    public float sprintSpeed = 20f;
    public float crouchSpeed = 1f;
    public float gravity = -9.81f;
    public float underWaterGravity = -4.81f;
    public float jumpHeight = 3f;
    public float health = 100;

    [Header("Ground Stats")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask ElevatorMask;

    [Header("Estadisticas")]
    public float crouchScale;
    private float defaultScale;

    [Space]
    public bool isGrounded;
    public bool isUnderWater;
    public bool isElevator;

    [Space]
    public float moveSpeed;
    Vector3 previousPosition;
    Vector3 velocity;
    float moveLength;

    [Header("Post Processing")]
    public GameObject Normal;
    public GameObject Under;


    private void Start()
    {
        defaultScale = transform.localScale.y;
    }

    void Update()
    {
        UpdatePostVol();
        anim.SetFloat("velocity", moveSpeed);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isElevator = Physics.CheckSphere(groundCheck.position, groundDistance, ElevatorMask);

        if (isGrounded && velocity.y < 0) { velocity.y = -2f; }
        if (isElevator) {  }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float multiSpeed;
        if (Input.GetButton("Sprint")) { multiSpeed = sprintSpeed; }
        else if (Input.GetButton("Crouch")) { multiSpeed = crouchSpeed; }
        else { multiSpeed = speed; }

        previousPosition = transform.position;
        controller.Move(move * multiSpeed * Time.deltaTime);
        moveLength = Vector3.Distance(previousPosition, transform.position);
        moveSpeed = moveLength / Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded) { 
            if (isUnderWater) { velocity.y = Mathf.Sqrt(jumpHeight * -2f * underWaterGravity); } else { velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); }
        }

        if (Input.GetButtonDown("Crouch")) { transform.localScale = new Vector3(transform.localScale.x, crouchScale, transform.localScale.z); }
        if (Input.GetButtonUp("Crouch")) { transform.localScale = new Vector3(transform.localScale.x, defaultScale, transform.localScale.z); }

        if (isUnderWater) { velocity.y += underWaterGravity * Time.deltaTime; } else { velocity.y += gravity * Time.deltaTime; }
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdatePostVol()
    {
        if (isUnderWater) { Normal.SetActive(false); Under.SetActive(true); EffectSound.SetActive(true); } 
        else { Normal.SetActive(true); Under.SetActive(false); EffectSound.SetActive(false); }
    }

    public void Damage(int amount)
    {
        health -= amount;
        CameraShaker.Instance.ShakeOnce(4, 4, .1f, .1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller Modifiers")]
    [SerializeField] float movementSpeed = 7.5f;
    [SerializeField] float jumpSpeed = 8.0f;
    [SerializeField] float gravity = 20.0f;
    [SerializeField] int healthAmount = 100;
  

    [Header("Camera Settings")]
    [SerializeField] Camera playerCamera;
    [SerializeField] [Range(0, 20)] float turnSpeed = 6;
    [SerializeField] float lookXLimit = 60.0f;

    [Header("UI Settings")]
    [SerializeField]
    [Tooltip("Text For health amount")]
    TextMeshProUGUI healthText;

    [Header("Gun Settings")]
    [SerializeField] Gun playerGun;


    private bool canMove = true;
    private Animator animator;
    private CharacterController controller;
    private Vector3 movedirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
        animator = gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        
    }

    /// Update player movement
    void PlayerMovement(float x, float y)
    {
        if (canMove)
        {
            
            if (controller.isGrounded)
            {
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                Vector3 right = transform.TransformDirection(Vector3.right);
                float curSpeedx = movementSpeed * x;
                float curSpeedy = movementSpeed * y;
                movedirection = (forward * curSpeedx) + (right * curSpeedy);

               

            }
            else
            {
                movedirection.y -= gravity * Time.deltaTime;

            }
            controller.Move(movedirection * Time.deltaTime);
        }
    }

    /// Update animator component
    void AnimatorUpdate(float x, float y, float speed = 1)
    {
        animator.SetFloat("yPos", Mathf.Clamp(x, -1, 1));
        animator.SetFloat("xPos", Mathf.Clamp(y, -1, 1));
        animator.SetFloat("speed", speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            playerGun.Shoot();

        }
        Collider[] cols = Physics.OverlapSphere(transform.position, 2);

        foreach (var col in cols)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                healthAmount -= 1;
            }
        }

        string htext = healthAmount.ToString();

        healthText.text = "|Health: " + htext;


        if (healthAmount <= 0) canMove = false;
        float fwd = 0;
        float side = Input.GetAxis("Horizontal");
        AnimatorUpdate(fwd, side, 1);
        PlayerMovement(fwd, side);
    }


    
}


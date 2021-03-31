using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller Modifiers")]
    [SerializeField] float movementSpeed = 7.5f;
    [SerializeField] float jumpSpeed = 8.0f;
    [SerializeField] float gravity = 20.0f;
  

    [Header("Camera Settings")]
    [SerializeField] Camera playerCamera;
    [SerializeField] [Range(0, 20)] float turnSpeed = 6;
    [SerializeField] float lookXLimit = 60.0f;



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
        
        if (controller.isGrounded)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedx = movementSpeed * x;
            float curSpeedy = movementSpeed * y;
            movedirection = (forward * curSpeedx) + (right * curSpeedy);

            if (Input.GetButton("Jump") && canMove)
            {
                movedirection.y = jumpSpeed;
            }
          
        }
        else
        {
            movedirection.y -= gravity * Time.deltaTime;
           
        }
        controller.Move(movedirection * Time.deltaTime);

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

        float fwd = 0;
        float side = Input.GetAxis("Horizontal");
        AnimatorUpdate(fwd, side, 1);
        PlayerMovement(fwd, side);
    }
}


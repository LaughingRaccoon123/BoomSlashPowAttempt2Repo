using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Mouse / Camera")]
    [SerializeField] private float mouseSensitivity = 2f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Camera")]
    [SerializeField] private Transform cameraAnchor;
    [SerializeField] private Vector3 anchorOffset = new Vector3(0f, 1.5f, 0f);

    private CharacterController controller;
    private Animator anim;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private float rotationY;
    private Vector3 playerVelocity;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        // --------------------
        // GROUND CHECK
        // --------------------

        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // --------------------
        // PLAYER ROTATION
        // --------------------

        rotationY += lookInput.x * mouseSensitivity;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        // --------------------
        // MOVEMENT
        // --------------------

        Vector3 moveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;

        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        controller.Move(moveDirection * speed * Time.deltaTime);


        // --------------------
        // JUMP
        // --------------------

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // --------------------
        // ANIMATION
        // --------------------

        if (anim != null)
        {
            bool isMoving = moveInput.sqrMagnitude > 0.01f;

            anim.SetBool("run", isMoving);
        }

        // --------------------
        // DETECTING ENEMY ATTACKS
        // --------------------

        
    }

    // =====================================================
    // INPUT SYSTEM
    // =====================================================

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.started)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moveInput = Vector2.zero;
        }   
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }


    private void LateUpdate()
    {
        if (cameraAnchor != null)
        {
            cameraAnchor.position = transform.position + anchorOffset;
        }
    }
}
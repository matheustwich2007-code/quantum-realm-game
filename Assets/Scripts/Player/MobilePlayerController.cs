using UnityEngine;

namespace QuantumRealm.Player
{
    public class MobilePlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float sprintSpeed = 9f;
        [SerializeField] private float jumpHeight = 1.5f;
        [SerializeField] private float gravity = -20f;

        [Header("Camera")]
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float mouseSensitivity = 2.5f;
        [SerializeField] private float lookClamp = 80f;

        private CharacterController controller;
        private Vector3 velocity;
        private float xRotation;
        private MobileTouchController mobileInput;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            mobileInput = GetComponent<MobileTouchController>();
            if (playerCamera == null)
            {
                playerCamera = Camera.main;
            }
        }

        private void Update()
        {
            if (Application.isMobilePlatform)
            {
                UpdateMobileMovement();
                UpdateMobileLook();
                return;
            }

            UpdateDesktopMovement();
            UpdateDesktopLook();
        }

        private void UpdateDesktopMovement()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 input = new Vector3(horizontal, 0f, vertical).normalized;
            Vector3 move = transform.TransformDirection(input) * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed);

            if (controller.isGrounded && velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            controller.Move(move * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        private void UpdateDesktopLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -lookClamp, lookClamp);

            if (playerCamera != null)
            {
                playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }

            transform.Rotate(Vector3.up * mouseX);
        }

        private void UpdateMobileMovement()
        {
            if (mobileInput == null)
            {
                return;
            }

            Vector3 input = new Vector3(mobileInput.MoveInput.x, 0f, mobileInput.MoveInput.y).normalized;
            Vector3 move = transform.TransformDirection(input) * (mobileInput.DashPressed ? sprintSpeed : moveSpeed);

            if (controller.isGrounded && velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            if (mobileInput.JumpPressed && controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            controller.Move(move * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        private void UpdateMobileLook()
        {
            if (mobileInput == null || playerCamera == null)
            {
                return;
            }

            float lookY = mobileInput.MoveInput.y * mouseSensitivity * 0.4f;
            xRotation -= lookY;
            xRotation = Mathf.Clamp(xRotation, -lookClamp, lookClamp);
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            float yaw = mobileInput.MoveInput.x * mouseSensitivity * 0.45f;
            transform.Rotate(Vector3.up * yaw);
        }
    }
}

using UnityEngine;

namespace QuantumRealm.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float sprintSpeed = 9f;
        [SerializeField] private float jumpHeight = 1.5f;
        [SerializeField] private float gravity = -20f;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private float mouseSensitivity = 2.5f;
        [SerializeField] private float maxLookUp = 80f;
        [SerializeField] private float maxLookDown = -80f;

        private CharacterController controller;
        private Vector3 velocity;
        private float xRotation;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            if (playerCamera == null)
            {
                playerCamera = Camera.main;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            UpdateMovement();
            UpdateLook();
        }

        private void UpdateMovement()
        {
            var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            bool sprinting = Input.GetKey(KeyCode.LeftShift);
            float speed = sprinting ? sprintSpeed : moveSpeed;

            Vector3 move = transform.TransformDirection(input.normalized);
            controller.Move(move * speed * Time.deltaTime);

            if (controller.isGrounded && velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        private void UpdateLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, maxLookDown, maxLookUp);

            if (playerCamera != null)
            {
                playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            }

            transform.Rotate(Vector3.up * mouseX);
        }
    }
}

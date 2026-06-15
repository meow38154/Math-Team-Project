using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]

    public class PlayerMovement : MonoBehaviour
    {
        [Header("Move")] [SerializeField] private float speed = 5f;

        [Header("Look")] [SerializeField] private Transform cameraRoot;
        [SerializeField] private float mouseSensitivity = 0.12f;
        [SerializeField] private float minPitch = -80f;
        [SerializeField] private float maxPitch = 80f;

        private Rigidbody _rb;

        private Vector2 _moveInput;
        private Vector2 _lookDelta;

        private float _yaw;
        private float _pitch;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            
            _rb.constraints = RigidbodyConstraints.FreezeRotationX |
                              RigidbodyConstraints.FreezeRotationZ;
        }

        private void Start()
        {
            _yaw = transform.eulerAngles.y;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            RotateView();
        }

        private void FixedUpdate()
        {
            Move();
            RotateBody();
        }

        private void RotateView()
        {
            if (_lookDelta.sqrMagnitude <= 0f)
                return;

            _yaw += _lookDelta.x * mouseSensitivity;
            _pitch -= _lookDelta.y * mouseSensitivity;
            _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

            if (cameraRoot != null)
                cameraRoot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);

            _lookDelta = Vector2.zero;
        }

        private void RotateBody()
        {
            Quaternion targetRotation = Quaternion.Euler(0f, _yaw, 0f);
            _rb.MoveRotation(targetRotation);
        }

        private void Move()
        {
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 moveDir = right * _moveInput.x + forward * _moveInput.y;

            if (moveDir.sqrMagnitude > 1f)
                moveDir.Normalize();

            Vector3 velocity = moveDir * speed;

            _rb.linearVelocity = velocity;
        }

        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void OnLook(InputValue value)
        {
            _lookDelta += value.Get<Vector2>();
        }
    }
}
using UnityEngine;
using Cinemachine;

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float sprintSpeed = 5f;
        [SerializeField] private float rotationSmoothTime = 0.12f;
        [SerializeField] private float speedChangeRate = 10.0f;

        [Header("Jump Settings")]
        [SerializeField] private float jumpHeight = 1.2f;
        [SerializeField] private float gravity = -15.0f;
        [SerializeField] private float jumpTimeout = 0.5f;
        [SerializeField] private float fallTimeout = 0.15f;

        [Header("Ground Check")]
        [SerializeField] private float groundedOffset = -0.14f;
        [SerializeField] private float groundedRadius = 0.28f;
        [SerializeField] private LayerMask groundLayers;

        [Header("Camera Settings")]
        [SerializeField] private GameObject cinemachineCameraTarget;
        [SerializeField] private Transform aimPointTransform;
        [SerializeField] private LayerMask aimColliderLayerMask;
        [SerializeField] private float topClamp = 70.0f;
        [SerializeField] private float bottomClamp = -30.0f;
        [SerializeField] private bool lockCameraPosition = false;

        private float _cinemachineYaw;
        private float _cinemachinePitch;
        private float _speed;
        private float _verticalVelocity;
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;
        private float _rotationVelocity;
        private float _targetRotation;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private Camera _mainCamera;
        private bool _grounded;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
        }

        private void Start()
        {
            _cinemachineYaw = cinemachineCameraTarget.transform.rotation.eulerAngles.y;
            _jumpTimeoutDelta = jumpTimeout;
            _fallTimeoutDelta = fallTimeout;
        }

        private void Update()
        {
            GroundedCheck();
            JumpAndGravity();
            Move();
            aimPointTransform.position = CalculateWorldAimPosition();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void GroundedCheck()
        {
            Vector3 spherePosition = transform.position + Vector3.down * groundedOffset;
            _grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
        }

        private void Move()
        {
            float targetSpeed = _input.sprint ? sprintSpeed : moveSpeed;
            targetSpeed = _input.move == Vector2.zero ? 0.0f : targetSpeed;

            float currentSpeed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;
            _speed = Mathf.Lerp(currentSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;

            Vector3 inputDirection = new Vector3(_input.move.x, 0, _input.move.y).normalized;
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0, rotation, 0);
            }

            Vector3 targetDirection = Quaternion.Euler(0, _targetRotation, 0) * Vector3.forward;
            Vector3 move = targetDirection.normalized * (_speed * Time.deltaTime) + Vector3.up * _verticalVelocity * Time.deltaTime;
            _controller.Move(move);
        }

        private void CameraRotation()
        {
            if (_input.look.sqrMagnitude >= 0.01f && !lockCameraPosition)
            {
                float deltaTimeMultiplier = _input.look.sqrMagnitude > 0.1f ? Time.deltaTime : 1.0f;
                _cinemachineYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachinePitch += _input.look.y * deltaTimeMultiplier;
            }

            _cinemachineYaw = ClampAngle(_cinemachineYaw, float.MinValue, float.MaxValue);
            _cinemachinePitch = ClampAngle(_cinemachinePitch, bottomClamp, topClamp);

            cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachinePitch, _cinemachineYaw, 0);
        }

        private void JumpAndGravity()
        {
            if (_grounded)
            {
                _fallTimeoutDelta = fallTimeout;
                if (_verticalVelocity < 0.0f)
                    _verticalVelocity = -2f;

                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                if (_jumpTimeoutDelta > 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                _jumpTimeoutDelta = jumpTimeout;
                if (_fallTimeoutDelta > 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }

                if (_verticalVelocity < 53.0f)
                {
                    _verticalVelocity += gravity * Time.deltaTime;
                }
            }
        }

        private Vector3 CalculateWorldAimPosition()
        {
            Ray ray = _mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
            if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayerMask))
            {
                return hit.point;
            }
            return ray.direction * 999f;
        }

        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }
    }
}

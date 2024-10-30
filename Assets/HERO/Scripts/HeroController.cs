using UnityEngine;
using Cinemachine;

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float MoveSpeed = 3f;
        public float SprintSpeed = 5f;
        public float RotationSmoothTime = 0.12f;
        public float SpeedChangeRate = 10.0f;

        [Header("Jump Settings")]
        public float JumpHeight = 1.2f;
        public float Gravity = -15.0f;
        public float JumpTimeout = 0.5f;
        public float FallTimeout = 0.15f;

        [Header("Ground Check")]
        public bool Grounded = true;
        public float GroundedOffset = -0.14f;
        public float GroundedRadius = 0.28f;
        public LayerMask GroundLayers;

        [Header("Camera Settings")]
        public GameObject CinemachineCameraTarget;
        public Transform AimPointTransform;
        public LayerMask AimColliderLayerMask;
        public float TopClamp = 70.0f;
        public float BottomClamp = -30.0f;
        public bool LockCameraPosition = false;

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

        private void Awake()
        {
            _mainCamera = Camera.main;
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
        }

        private void Start()
        {
            _cinemachineYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        private void Update()
        {
            GroundedCheck();
            JumpAndGravity();
            Move();
            AimPointTransform.position = CalculateWorldAimPosition();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void GroundedCheck()
        {
            Vector3 spherePosition = transform.position + Vector3.down * GroundedOffset;
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        }

        private void Move()
        {
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
            targetSpeed = _input.move == Vector2.zero ? 0.0f : targetSpeed;

            float currentSpeed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;
            _speed = Mathf.Lerp(currentSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;

            Vector3 inputDirection = new Vector3(_input.move.x, 0, _input.move.y).normalized;
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                transform.rotation = Quaternion.Euler(0, rotation, 0);
            }

            Vector3 targetDirection = Quaternion.Euler(0, _targetRotation, 0) * Vector3.forward;
            Vector3 move = targetDirection.normalized * (_speed * Time.deltaTime) + Vector3.up * _verticalVelocity * Time.deltaTime;
            _controller.Move(move);
        }

        private void CameraRotation()
        {
            if (_input.look.sqrMagnitude >= 0.01f && !LockCameraPosition)
            {
                float deltaTimeMultiplier = _input.look.sqrMagnitude > 0.1f ? Time.deltaTime : 1.0f;
                _cinemachineYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachinePitch += _input.look.y * deltaTimeMultiplier;
            }

            _cinemachineYaw = ClampAngle(_cinemachineYaw, float.MinValue, float.MaxValue);
            _cinemachinePitch = ClampAngle(_cinemachinePitch, BottomClamp, TopClamp);

            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachinePitch, _cinemachineYaw, 0);
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                _fallTimeoutDelta = FallTimeout;
                if (_verticalVelocity < 0.0f)
                    _verticalVelocity = -2f;

                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                }

                if (_jumpTimeoutDelta > 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                _jumpTimeoutDelta = JumpTimeout;
                if (_fallTimeoutDelta > 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }

                if (_verticalVelocity < 53.0f)
                {
                    _verticalVelocity += Gravity * Time.deltaTime;
                }
            }
        }

        private Vector3 CalculateWorldAimPosition()
        {
            Vector3 mouseWorldPosition = Vector3.zero;
            Ray ray = _mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
            if (Physics.Raycast(ray, out RaycastHit hit, 999f, AimColliderLayerMask))
            {
                mouseWorldPosition = hit.point;
            }
            else
            {
                mouseWorldPosition = ray.direction * 999f;
            }
            return mouseWorldPosition;
        }

        private static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360f) angle += 360f;
            if (angle > 360f) angle -= 360f;
            return Mathf.Clamp(angle, min, max);
        }
    }
}

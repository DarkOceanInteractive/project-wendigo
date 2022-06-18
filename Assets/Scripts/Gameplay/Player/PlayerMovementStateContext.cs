using UnityEngine;

namespace ProjectWendigo
{
    public class PlayerMovementStateContext : AStateContext
    {
        public float CrouchingSpeedMultiplier = 0.7f;
        [HideInInspector] public float CrouchStartTime;
        public float MovementSpeed = 4f;
        [HideInInspector] public float SpeedMultiplier = 1f;
        public float SprintingSpeedMultiplier = 1.5f;
        [HideInInspector] public float StartHeight = 1f;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerController _playerController;
        public float SmoothSpeedMultiplier { get; private set; } = 1f;

        public bool IsGrounded => this._characterController.isGrounded;
        public float Height
        {
            get => this._characterController.height;
            set => this._characterController.height = value;
        }

        public void Start()
        {
            this.StartHeight = this.Height;
            this.SetState(new PlayerMovementStates.Idle());
        }

        protected override void Update()
        {
            this.SmoothSpeedMultiplier = Mathf.Lerp(this.SmoothSpeedMultiplier, this.SpeedMultiplier, 4f * Time.deltaTime);
            base.Update();
            this.Move();

            Vector3 moveVector = Vector3.zero;

            if (this.IsGrounded == false) {
                moveVector += Physics.gravity;
            }

            _characterController.Move(moveVector * Time.deltaTime);
        }

        /// <summary>
        /// Move the character controller by `motion`.
        /// </summary>
        /// <param name="motion">Motion to apply</param>
        public void Move(Vector3 motion)
        {
            this._playerController.Move(motion);
        }

        private void Move()
        {
            Vector2 move = Singletons.Main.Input.PlayerMovement;
            Vector3 movement = this.transform.TransformDirection(
                move.x * this.MovementSpeed * this.SmoothSpeedMultiplier,
                0f,
                move.y * this.MovementSpeed * this.SmoothSpeedMultiplier
            );
            this.Move(movement * Time.deltaTime);
        }
    }
}

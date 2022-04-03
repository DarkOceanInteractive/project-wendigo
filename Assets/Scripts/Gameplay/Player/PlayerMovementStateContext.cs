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
        private float _smoothSpeedMultiplier = 1f;

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
            this._smoothSpeedMultiplier = Mathf.Lerp(this._smoothSpeedMultiplier, this.SpeedMultiplier, 4f * Time.deltaTime);
            base.Update();
            this.Move();
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
                move.x * this.MovementSpeed * this._smoothSpeedMultiplier,
                0f,
                move.y * this.MovementSpeed * this._smoothSpeedMultiplier
            );
            this.Move(movement * Time.deltaTime);
        }
    }
}

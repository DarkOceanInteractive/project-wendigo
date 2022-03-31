using UnityEngine;

namespace ProjectWendigo
{
    public class PlayerJumpStateContext : AStateContext
    {
        public float GravityMultiplier = 1.5f;
        public float JumpHeight = 1.2f;
        [HideInInspector] public float VerticalSpeed = 0f;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerController _playerController;

        public bool IsGrounded => this._characterController.isGrounded;

        public void Start()
        {
            this.SetState(new PlayerJumpStates.Idle());
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            this.Move();
        }

        private void Move()
        {
            var movement = new Vector3(
                0f,
                this.VerticalSpeed,
                0f
            );
            this._playerController.Move(movement * Time.deltaTime);
        }
    }
}

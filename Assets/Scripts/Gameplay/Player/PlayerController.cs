using UnityEngine;

namespace ProjectWendigo
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 4.0f;
        public float sprintSpeedMultiplier = 1.5f;
        public float crouchSpeedMultiplier = 0.5f;
        public float jumpHeight = 1.2f;
        private float verticalSpeed = 0.0f;
        private bool isCrouching = false;
        private float crouchStartTime;
        private float crouchHeightMultiplier = 0.7f;
        private float crouchStartHeight;
        private float startHeight;
        public float crouchTransitionDuration = 0.5f;
        [SerializeField] private CharacterController controller;

        public void Start()
        {
            this.startHeight = this.controller.height;
        }

        public void Update()
        {
            // Crouching
            float newHeight = this.startHeight;
            float elapsedTime = Time.time - this.crouchStartTime;
            if (Input.GetButtonDown("Crouch"))
            {
                this.isCrouching = true;
                this.crouchStartHeight = this.startHeight;
                float remainingTime = Mathf.Max(this.crouchTransitionDuration - elapsedTime, 0f);
                this.crouchStartTime = Time.time - remainingTime;
                elapsedTime = Time.time - this.crouchStartTime;
            }
            if (Input.GetButton("Crouch"))
            {
                newHeight = this.crouchHeightMultiplier * this.startHeight;
            }
            if (Input.GetButtonUp("Crouch"))
            {
                this.crouchStartHeight = this.startHeight * this.crouchHeightMultiplier;
                float remainingTime = Mathf.Max(this.crouchTransitionDuration - elapsedTime, 0f);
                this.crouchStartTime = Time.time - remainingTime;
                elapsedTime = Time.time - this.crouchStartTime;
            }
            float t = elapsedTime / this.crouchTransitionDuration;
            if (this.isCrouching)
            {
                if (t <= 1f)
                {
                    float lastHeight = this.controller.height;
                    this.controller.height = Mathf.Lerp(this.crouchStartHeight, newHeight, t);
                    this.controller.Move(new Vector3(0f, (this.controller.height - lastHeight) * 0.5f, 0f));
                }
                else if (!Input.GetButton("Crouch"))
                {
                    this.isCrouching = false;
                }
            }

            // Jump and fall
            if (this.controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                    // To jump at height h: v.y = sqrt(h * 2 * g) As Physics.gravity.y is already negative, v.y = sqrt(h * 2 * -g)
                    this.verticalSpeed = Mathf.Sqrt(this.jumpHeight * 2f * -Physics.gravity.y);
                else
                    // Reset vertical speed so it doesn't add up when grounded
                    this.verticalSpeed = -1f;
            }
            else
            {
                // v(t).y = -g * t => deltaV(t).y = -g * deltaT
                // As Physics.gravity.y is already negative, deltaV(t).y = -(-g) * deltaT = g * deltaT
                this.verticalSpeed += Physics.gravity.y * Time.deltaTime;
            }

            float speedMultiplier = 1.0f;
            if (this.isCrouching)
                speedMultiplier = this.crouchSpeedMultiplier;
            else if (Input.GetKey(KeyCode.LeftShift))
                speedMultiplier = this.sprintSpeedMultiplier;

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 movement = this.transform.TransformDirection(
                x * this.moveSpeed * speedMultiplier,
                this.verticalSpeed,
                z * this.moveSpeed * speedMultiplier
            );
            this.controller.Move(movement * Time.deltaTime);
        }
    }
}

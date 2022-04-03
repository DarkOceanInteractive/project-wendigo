using UnityEngine;

namespace ProjectWendigo.PlayerMovementStates
{
    public class Crouching : AState<PlayerMovementStateContext>
    {
        private CharacterController _characterController;
        private float _crouchHeightMultiplier = 0.7f;
        private float _crouchStartHeight;
        private float _crouchTransitionDuration = 0.3f;
        private float _elapsedTime;

        public override void Enter()
        {
            this._characterController = this.context.GetComponent<CharacterController>();
            this.context.SpeedMultiplier = this.context.CrouchingSpeedMultiplier;

            this.BeginCrouch();
        }

        public override void Update()
        {
            float newHeight = this.context.StartHeight;
            this._elapsedTime = Time.time - this.context.CrouchStartTime;

            if (Singletons.Main.Input.PlayerStartedCrouching)
                this.BeginCrouch();
            if (Singletons.Main.Input.PlayerIsCrouching)
                newHeight = this._crouchHeightMultiplier * this.context.StartHeight;
            else if (Singletons.Main.Input.PlayerStoppedCrouching)
                this.EndCrouch();

            float t = this._elapsedTime / this._crouchTransitionDuration;
            if (t <= 1f)
            {
                float lastHeight = this.context.Height;
                this.context.Height = Mathf.Lerp(this._crouchStartHeight, newHeight, t);
                _ = this._characterController.Move(new Vector3(0f, (this.context.Height - lastHeight) * 0.5f, 0f));
            }
            else if (!Singletons.Main.Input.PlayerIsCrouching)
            {
                this.context.SetState(new Idle());
                return;
            }
        }

        private void ResetCrouchTimer()
        {
            float remainingTime = Mathf.Max(this._crouchTransitionDuration - this._elapsedTime, 0f);
            this.context.CrouchStartTime = Time.time - remainingTime;
            this._elapsedTime = Time.time - this.context.CrouchStartTime;
        }

        private void BeginCrouch()
        {
            this._elapsedTime = Time.time - this.context.CrouchStartTime;
            this._crouchStartHeight = this.context.StartHeight;
            this.ResetCrouchTimer();
        }

        private void EndCrouch()
        {
            this._crouchStartHeight = this.context.StartHeight * this._crouchHeightMultiplier;
            this.ResetCrouchTimer();
        }
    }
}

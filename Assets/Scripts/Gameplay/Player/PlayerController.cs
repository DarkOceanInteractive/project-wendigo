using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 4.0f;
        public float sprintSpeedMultiplier = 1.5f;
        public float jumpHeight = 1.2f;
        private float verticalSpeed = 0.0f;
        [SerializeField] private CharacterController controller;

        public void Update()
        {
            if (this.controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                    // To jump at height h: v.y = sqrt(h * 2 * g)
                    // As Physics.gravity.y is already negative, v.y = sqrt(h * 2 * -g)
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

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 movement = this.transform.TransformDirection(
                x * this.moveSpeed,
                this.verticalSpeed,
                z * this.moveSpeed
            );
            this.controller.Move(movement * Time.deltaTime);
        }
    }
}

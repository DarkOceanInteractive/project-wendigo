using UnityEngine;

namespace ProjectWendigo
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        private Vector3 _motion = Vector3.zero;

        public void Update()
        {
            if (this._motion != Vector3.zero)
            {
                this._characterController.Move(this._motion);
                this._motion = Vector3.zero;
            }
        }

        /// <summary>
        /// Move the character controller by `motion`
        /// </summary>
        /// <param name="motion">Motion to apply</param>
        public void Move(Vector3 motion)
        {
            this._motion += motion;
        }
    }
}

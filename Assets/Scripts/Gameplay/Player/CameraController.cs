using UnityEngine;

namespace ProjectWendigo
{
    public class CameraController : MonoBehaviour
    {
        public float mouseSensitivity = 500f;
        private float xRotation = 0f;
        [SerializeField] private Transform playerBodyTransform;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * this.mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * this.mouseSensitivity * Time.deltaTime;

            this.xRotation = Mathf.Clamp(this.xRotation - mouseY, -90f, 90f);

            this.playerBodyTransform.Rotate(Vector3.up * mouseX);
            this.transform.localRotation = Quaternion.Euler(this.xRotation, 0f, 0f);
        }
    }
}

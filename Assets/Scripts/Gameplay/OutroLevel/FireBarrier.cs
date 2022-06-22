using System.Collections;
using UnityEngine;

namespace ProjectWendigo
{
    public class FireBarrier : MonoBehaviour
    {
        private ParticleSystem _particles;

        [SerializeField] public float _mass = 3.0f;
        [SerializeField] private float _force = 50f;
        [SerializeField] private Vector3 _direction;
        private Vector3 _impact = Vector3.zero;
        private CharacterController _characterController;

        void Start()
        {
            this._particles = this.GetComponent<ParticleSystem>();
            var collision = this._particles.collision;
            collision.enabled = true;
            collision.type = ParticleSystemCollisionType.World;
            collision.mode = ParticleSystemCollisionMode.Collision3D;
            collision.sendCollisionMessages = true;
            this._characterController = Singletons.Main.Player.PlayerBody.GetComponent<CharacterController>();
        }

        void OnParticleCollision(GameObject other)
        {
            if (other.tag == "Player")
                this.AddImpact(this._force);
        }

        private void AddImpact(float force)
        {
            this._direction.Normalize();
            this._impact += this._direction.normalized * force / this._mass;
        }

        private void FixedUpdate()
        {
            if (this._impact.magnitude > 0.2)
                this._characterController.Move(this._impact * Time.deltaTime);
            this._impact = Vector3.Lerp(this._impact, Vector3.zero, 5 * Time.deltaTime);
        }
    }
}

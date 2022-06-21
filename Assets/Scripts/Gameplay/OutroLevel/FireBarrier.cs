using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBarrier : MonoBehaviour
{
    private ParticleSystem particles;
    private GameObject player;

    public float mass = 3.0f;
    private Vector3 impact = Vector3.zero;
    private CharacterController _characterController;
    public float force = 50f;
    public Vector3 direction;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        var collision = particles.collision;
        collision.enabled = true;
        collision.type = ParticleSystemCollisionType.World;
        collision.mode = ParticleSystemCollisionMode.Collision3D;
        collision.sendCollisionMessages = true;


        player = GameObject.FindGameObjectWithTag("Player");
        _characterController = player.GetComponent<CharacterController>();
        Debug.Log(particles);
    }

    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
            AddImpact(force);

    }

    private void AddImpact(float force)
    {
        direction.Normalize();
        impact += direction.normalized * force / mass;
    }

    void Update()
    {
        if(impact.magnitude > 0.2)
            _characterController.Move(impact * Time.deltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }
}

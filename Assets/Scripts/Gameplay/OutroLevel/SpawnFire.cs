using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public class SpawnFire : MonoBehaviour
    {
        public GameObject[] crystals = new GameObject[4];
        public ParticleSystem[] fireParticles;
        private ParticleSystem fireBarrier;
        private bool crystalDestroyed = true;

        // Start is called before the first frame update
        void Start()
        {
            for(int i = 0; i < fireParticles.Length; i++)
                {
                    fireParticles[i].Stop();
                }
            fireBarrier = GetComponent<ParticleSystem>();
            fireBarrier.Play();
            // particles.Stop();
        }

        // Update is called once per frame
        void Update()
        {
            CheckCrystals();
            if(crystalDestroyed)
                PlayFire();
            
        }

        private void CheckCrystals()
        {
            for(int i = 0; i < crystals.Length; i++)
            {
                if(crystals[i].active)
                {
                    crystalDestroyed = false;
                    break;
                }else {
                    crystalDestroyed = true;
                }
            }
        }

        private void PlayFire()
        {
            for(int i = 0; i < fireParticles.Length; i++)
            {
                if(!fireParticles[i].isPlaying)
                    fireParticles[i].Play();
            }

            if(fireBarrier.isPlaying)
                fireBarrier.Stop();
        }
    }
}
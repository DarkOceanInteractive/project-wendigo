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
        public GameObject portal;

        void Start()
        {
            // Stop particles 
            for(int i = 0; i < fireParticles.Length; i++)
                {
                    fireParticles[i].Stop();
                }
            // Start fire particle barrier
            fireBarrier = GetComponent<ParticleSystem>();
            fireBarrier.Play();
        }

        void Update()
        {
            CheckCrystals();
            if(crystalDestroyed)
                PlayFire();
        }

        // Method to check if all crystals are destroyed
        private void CheckCrystals()
        {
            for(int i = 0; i < crystals.Length; i++)
            {
                if(crystals[i].activeSelf == true)
                {
                    crystalDestroyed = false;
                    break;
                }else {
                    crystalDestroyed = true;
                }
            }
        }

        // Method to stop the firebarrier and start playing the fire particles
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
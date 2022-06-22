using UnityEngine;

namespace ProjectWendigo
{
    public class InteractableCrystal : Breakable
    {
        [SerializeField] private ParticleSystem powerBeam;

        public override void OnInteract(GameObject target)
        {
            base.OnInteract(target);
            // Stop the power beam particles that came from the crystal object
            if (this.powerBeam.isPlaying)
                powerBeam.Stop();
        }
    }
}
using UnityEngine;

namespace ProjectWendigo
{
    [RequireComponent(typeof(Renderer))]
    public class Dissolve : MonoBehaviour
    {
        [SerializeField] private float _disappearingHeight = 14f;
        [SerializeField] private float _noiseStrength = 0.25f;
        private Material _material;

        private void Awake()
        {
            this._material = GetComponent<Renderer>().material;
            this.SetHeight(this._disappearingHeight);
        }

        private void SetHeight(float height)
        {
            this._material.SetFloat("_CutoffHeight", height);
            this._material.SetFloat("_NoiseStrength", this._noiseStrength);
        }
    }
}
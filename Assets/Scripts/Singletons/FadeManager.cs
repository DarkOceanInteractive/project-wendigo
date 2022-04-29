using UnityEngine;

namespace ProjectWendigo
{
    public class FadeManager : MonoBehaviour
    {
        public Animator Animator;

        public void FadeOutEffect()
        {
            this.Animator.SetTrigger("FadeOut");
        }

        public void FadeInEffect()
        {
            this.Animator.SetTrigger("FadeIn");
        }
    }
}

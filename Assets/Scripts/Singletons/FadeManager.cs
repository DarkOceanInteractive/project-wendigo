using System.Collections;
using UnityEngine;

namespace ProjectWendigo
{
    public class FadeManager : MonoBehaviour
    {
        public Animator Animator;

        public IEnumerator WaitForFadeOutEffect()
        {
            this.FadeOutEffect();
            yield return new WaitForSecondsRealtime(this.Animator.GetCurrentAnimatorClipInfo(0).Length);
        }
        public void FadeOutEffect()
        {
            this.Animator.SetTrigger("FadeOut");
        }

        public IEnumerator WaitForFadeInEffect()
        {
            this.FadeInEffect();
            yield return new WaitForSecondsRealtime(this.Animator.GetCurrentAnimatorClipInfo(0).Length);
        }
        public void FadeInEffect()
        {
            this.Animator.SetTrigger("FadeIn");
        }
    }
}

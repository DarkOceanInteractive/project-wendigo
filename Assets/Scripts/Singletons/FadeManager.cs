using UnityEngine;


namespace ProjectWendigo
{
    
    public class FadeManager : MonoBehaviour
    {

        public Animator animator;

        public void FadeOutEffect(){
            animator.SetTrigger("FadeOut");
        }

        public void FadeInEffect(){
            animator.SetTrigger("FadeIn");
        }
    }
}

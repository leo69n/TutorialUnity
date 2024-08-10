using System.Collections;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    Animator AnimatorComponent;
    private void Awake()
    {
        AnimatorComponent = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(PrepareNonLoopAnimation());
    }
    IEnumerator PrepareNonLoopAnimation()
    {
        var CurrentAnimationInfo = AnimatorComponent.GetCurrentAnimatorStateInfo(0); //get current Animation info
        var AnimationDuration = CurrentAnimationInfo.length; //get Duration of this animation
        yield return new WaitForSeconds(AnimationDuration);
        Destroy(this.gameObject);
    }
}

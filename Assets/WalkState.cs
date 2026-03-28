using UnityEngine;

public class IdleWalkState : StateMachineBehaviour
{
    float time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        if (time > 10) //sau 10 giay thi set co tuan tra la true
            animator.SetBool("isPatrolling", false);
    }
}

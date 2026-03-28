using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    float time;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        if (time > 5) //sau 5 giay thi set co tuan tra la true
            animator.SetBool("isPatrolling", true);
    }
}
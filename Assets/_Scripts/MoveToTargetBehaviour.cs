using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetBehaviour : StateMachineBehaviour
{
    private Transform _target;
    private float _range;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RangedAttack rangedAttack = animator.GetComponent<RangedAttack>();
        _range = rangedAttack.range;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_target)
        {
            FindTarget();
        }

        if (_target && Vector2.Distance(_target.position, animator.transform.position) < _range)
        {
            animator.SetBool("TargetInRange",true);
        }
    }

    void FindTarget()
    {
        // TODO: find according to distance
        var rootTrans = FindObjectOfType<Root>().transform;
        var playerTrans = FindObjectOfType<PlayerTurret>().transform;
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtTargetBehaviour : StateMachineBehaviour
{
    private Transform _target;
    private float _range;
    private RangedAttack rangedAttack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rangedAttack = animator.GetComponent<RangedAttack>();
        _range = rangedAttack.range;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    float fireTime = 0;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_target && Vector2.Distance(_target.position, animator.transform.position) < _range)
        {
            if (Time.time > fireTime)
            {
                // shoot
                fireTime += rangedAttack.secondsBetweenFire;
            }
        }
        else
        {
            animator.SetBool("TargetInRange", false);
        }
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
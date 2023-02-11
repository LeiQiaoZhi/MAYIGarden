using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MoveToTargetBehaviour : StateMachineBehaviour
{
    private float _range;
    private RangedAttack _rangedAttack;
    private Rigidbody2D _rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rangedAttack = animator.GetComponent<RangedAttack>();
        _range = _rangedAttack.range;
        _rb = _rangedAttack.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform target = _rangedAttack.target;
        if (!target)
        {
            FindTarget(animator.transform.position);
        }
        else
        {
            RotateHelper.RotateTowards(_rb,target,_rb.transform.up,_rangedAttack.rotateSpeed);
        }

        if (target && Vector2.Distance(target.position, animator.transform.position) < _range)
        {
            Debug.LogWarning("Target In Range!");
            _rangedAttack.Stop();
            animator.SetBool("TargetInRange", true);
        }
    }

    void FindTarget(Vector3 position)
    {
        var rootTrans = FindObjectOfType<Root>().transform;
        var playerTrans = FindObjectOfType<PlayerTurret>().transform;

        Debug.LogWarning("Finding Target for Ranged Enemy");
        if (Vector2.Distance(rootTrans.position, position) < Vector2.Distance(playerTrans.position, position))
        {
            _rangedAttack.SetTarget(rootTrans);
        }
        else
        {
            _rangedAttack.SetTarget(playerTrans);
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
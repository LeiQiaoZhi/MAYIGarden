using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtTargetBehaviour : StateMachineBehaviour
{
    private float _range;
    private RangedAttack _rangedAttack;
    private Rigidbody2D _rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rangedAttack = animator.GetComponent<RangedAttack>();
        _range = _rangedAttack.range;
        _rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    private float _fireTime = 0;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform target = _rangedAttack.target;
        if (target && Vector2.Distance(target.position, animator.transform.position) < _range)
        {
            var angleBetween =
                RotateHelper.AngleBetween(_rb.transform.position, target.position, animator.transform.up);
            if (Time.time > _fireTime && angleBetween<10)
            {
                // shoot
                Debug.LogWarning($"Shoot at {target}");
                _rangedAttack.Shoot();
                _fireTime = Time.time + _rangedAttack.secondsBetweenFire;
            }
            else
            {
                var direction = (Vector2)(target.position - _rb.transform.position).normalized;
                float rotateAmount = Vector3.Cross(direction, _rb.transform.up).z;
                _rb.angularVelocity = -rotateAmount * _rangedAttack.rotateSpeed;
            }
        }
        else
        {
            Debug.LogWarning("Target Out of Range");
            animator.SetBool("TargetInRange", false);
            // resume walking
            _rangedAttack.SetTarget(null);
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
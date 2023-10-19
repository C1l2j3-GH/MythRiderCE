using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFNina_Animation
{
    const string _currentFormNum = "currentFormNum";

    #region BF_Animations
    const string _isIdle = "isIdle";
    const string _isMoving = "isMoving";
    const string _isDashing = "isDashing";
    const string _isJumping = "isJumping";
    const string _isDropping = "isDropping";
    const string _isShooting = "isShooting";
    const string _isDashShooting = "isDashShooting";
    const string _isMoveShooting = "isMoveShooting";
    const string _isTakeDamage = "isTakeDamage";
    const string _isLWallSliding = "isLWallSliding";
    const string _isLWSShooting = "isLWSShooting";
    const string _isRWallSliding = "isRWallSliding";
    const string _isRWSShooting = "isRWSShooting";
    //Extra for HA
    const string _isInAir = "isInAir";
    const string _isSShooting = "isSShooting";
    const string _isMSShooting = "isMSShooting";

    #endregion

    #region HA_Animations
    const string _f1IsMoving = "IsMoving";
    const string _f1IsDashing = "f1IsDashing";
    const string _f1IsJumping = "f1IsJumping";
    const string _f1IsDropping = "f1IsDropping";
    const string _f1IsShooting = "f1IsShooting";
    const string _f1IsDashShooting = "f1IsDashShooting";
    const string _f1IsMoveShooting = "f1IsMoveShooting";
    const string _f1IsTakeDamage = "f1IsTakeDamage";
    const string _f1IsWallSliding = "f1IsWallSliding";
    const string _f1IsWSShooting = "f1IsWSShooting";
    #endregion

    public static void SetAnimatorBool(Animator animator, string boolName, bool boolCondition)
    {
        animator.SetBool(boolName, boolCondition);
    }

    public static void SetAnimatorInt(Animator animator, string intName, int currentFormNum)
    {
        animator.SetInteger(intName, currentFormNum);
    }
    public static void Check_SetBool(int currentFormNum, Animator animator, string boolName1, string boolName2, bool boolCondition)
    {
        switch (currentFormNum)
        {
            default:
                animator.SetBool(boolName1, boolCondition);
                break;
            case 2:
                animator.SetBool(boolName2, boolCondition);
                break;
        }
    }
    //public void Check_SetBool(int currentFormNum, Animator animator, string boolName1, string boolName2, string boolName3, string boolName4, bool boolCondition)
    //{
    //    switch (currentFormNum)
    //    {
    //        default:
    //            animator.SetBool(boolName1, boolCondition);
    //            break;
    //        case 2:
    //            animator.SetBool(boolName2, boolCondition);
    //            break;
    //        case 3:
    //            animator.SetBool(boolName3, boolCondition);
    //            break;
    //        case 4:
    //            animator.SetBool(boolName4, boolCondition);
    //            break;
    //    }
    //}
}
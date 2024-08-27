using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        var enemyDamagedController = GetComponent<EnemyDamagedController>();
        if (enemyDamagedController != null)
        {
            enemyDamagedController.isDead += PlayDieAnim;
        }

        var enemyChaseController = GetComponent<EnemyChaseController>();
        if (enemyChaseController != null) 
        {
            enemyChaseController.PlayerChase += PlayStandingAnim;
            enemyChaseController.NotChase += PlaySitdownAnim;
        }

        var enemyNavMeshController = GetComponent<EnemyNavMeshController>();
        if (enemyNavMeshController != null)
        {
            enemyNavMeshController.isChasingRun += PlayRunningAnim;
            enemyNavMeshController.isAttackDistance += PlayAttack1Anim;
        }
    }

    void PlayDieAnim()
    {
        animator.SetTrigger("Die");
    }

    void PlayStandingAnim() 
    {
        animator.SetTrigger("isLook");
    }

    void PlaySitdownAnim()
    {
        animator.SetTrigger("NotChase");
        animator.ResetTrigger("isRunnig");
    }

    void PlayRunningAnim()
    {
        animator.SetTrigger("isRunnig");
    }

    void PlayAttack1Anim()
    {
        animator.ResetTrigger("isRunnig");
        animator.SetTrigger("Attack1");
    }

    private void OnDisable()
    {
        var enemyDamagedController = GetComponent<EnemyDamagedController>();
        if (enemyDamagedController != null)
        {
            enemyDamagedController.isDead -= PlayDieAnim;
        }

        var enemyChaseController = GetComponent<EnemyChaseController>();
        if (enemyChaseController != null)
        {
            enemyChaseController.PlayerChase -= PlayStandingAnim;
            enemyChaseController.NotChase -= PlaySitdownAnim;
        }

        var enemyNavMeshController = GetComponent<EnemyNavMeshController>();
        if (enemyNavMeshController != null)
        {
            enemyNavMeshController.isChasingRun -= PlayRunningAnim;
        }
    }
}

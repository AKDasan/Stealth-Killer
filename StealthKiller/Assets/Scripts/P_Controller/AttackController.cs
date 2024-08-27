using System;
using System.Collections;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Animator animator;
    private bool isKeyDown = false;
    private bool isAttack1 = true;

    public static event Action Attack1;
    public static event Action Attack2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isKeyDown)
        {
            StartCoroutine(AttackAnimController());
        }
    }

    IEnumerator AttackAnimController()
    {
        isKeyDown = true;
        isAttack1 = !isAttack1;

        if (isAttack1)
        {
            animator.SetTrigger("Attack1");
            Attack1?.Invoke();
        }
        else
        {
            animator.SetTrigger("Attack2");
            Attack2?.Invoke();
        }

        yield return new WaitForSeconds(0.50f);
        isKeyDown = false;
    }
}

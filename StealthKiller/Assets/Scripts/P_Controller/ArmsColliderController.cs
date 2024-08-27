using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmsColliderController : MonoBehaviour
{
    [SerializeField] private Collider LeftArmCollider;
    [SerializeField] private Collider RightArmCollider;

    private void Awake()
    {
        LeftArmCollider.enabled = false;
        RightArmCollider.enabled = false;
    }

    private void Start()
    {
        AttackController.Attack1 += OnAttack1;
        AttackController.Attack2 += OnAttack2;
    }

    IEnumerator LeftColliderOn()
    {
        LeftArmCollider.enabled = true;

        yield return new WaitForSeconds(0.50f);
        LeftColliderOff();
    }

    void LeftColliderOff()
    {
        LeftArmCollider.enabled = false;
    }

    void OnAttack1()
    {
        StartCoroutine(LeftColliderOn());
    }

    IEnumerator RightColliderOn()
    {
        RightArmCollider.enabled = true;

        yield return new WaitForSeconds(0.50f);
        RightColliderOff();
    }

    void RightColliderOff() 
    {
        RightArmCollider.enabled = false;
    }

    void OnAttack2()
    {
        StartCoroutine(RightColliderOn());
    }

    private void OnDisable()
    {
        AttackController.Attack1 -= OnAttack1;
        AttackController.Attack2 -= OnAttack2;
    }
}

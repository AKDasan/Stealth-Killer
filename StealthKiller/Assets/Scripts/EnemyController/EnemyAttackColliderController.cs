using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackColliderController : MonoBehaviour
{
    [SerializeField] private Collider EnemyFistCollider;

    private void Start()
    {
        EnemyFistCollider.enabled = false;
    }

    // Goblin Attack1 animasyonuna eklendi event olarak
    void EnemyFistEnabled()
    {
        EnemyFistCollider.enabled = true;
        StartCoroutine(EnemyFistDisable());
    }

    IEnumerator EnemyFistDisable()
    {
        yield return new WaitForSeconds(0.75f);
        EnemyFistCollider.enabled = false;
    }
}

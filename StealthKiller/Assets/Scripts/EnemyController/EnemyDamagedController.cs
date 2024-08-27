using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagedController : MonoBehaviour
{
    [SerializeField] private float Health = 100f;
    [SerializeField] private bool isDamageable = true;

    public event Action isDamaged;
    public event Action isDead;

    private bool isCoroutinePlay = false;

    private void Update()
    {
        if (Health <= 0 && !isCoroutinePlay)
        {
            StartCoroutine(EnemyDeath());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blade") && isDamageable)
        {
            Health -= 25f;
            isDamaged?.Invoke();
            isDamageable = false;
            StartCoroutine(IsDamageableController());
        }
    }

    IEnumerator IsDamageableController()
    {
        yield return new WaitForSeconds(0.50f);
        isDamageable = true;
    }

    IEnumerator EnemyDeath()
    {
        isCoroutinePlay = true;
        isDead?.Invoke();
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

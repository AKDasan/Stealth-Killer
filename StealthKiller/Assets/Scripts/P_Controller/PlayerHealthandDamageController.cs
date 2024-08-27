using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthandDamageController : MonoBehaviour
{
    [SerializeField] private float Health = 100f;
    [SerializeField] private bool isDamageable = true;

    public static event Action isDamaged;

    private bool isCoroutinePlay = false;

    public float health
    {
        get { return Health; }
    }

    private void Update()
    {
        if (Health <= 0)
        {
           PlayerDeath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack") && isDamageable)
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

    void PlayerDeath()
    {
        SceneManager.LoadScene(0);
    }
}

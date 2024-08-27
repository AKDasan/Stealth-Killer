using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    [SerializeField] AudioSource e_AudioSource;

    [SerializeField] AudioClip EnemyDamagedSound;
    [SerializeField] AudioClip EnemyDeathSound;
    [SerializeField] AudioClip EnemyChaseSound;

    private void Start()
    {
        var enemyDamagedController = GetComponent<EnemyDamagedController>();
        if (enemyDamagedController != null)
        {
            enemyDamagedController.isDamaged += PlayEnemyDamagedSound;
            enemyDamagedController.isDead += PlayEnemyDeathSound;
        }

        var enemyChaseController = GetComponent<EnemyChaseController>();
        if (enemyChaseController != null)
        {
            enemyChaseController.PlayerChase += PlayEnemyChaseSound;
        }
    }

    void PlayEnemyDamagedSound()
    {
        if (e_AudioSource != null)
        {
            e_AudioSource.PlayOneShot(EnemyDamagedSound);
        }
    }

    void PlayEnemyDeathSound()
    {
        if (e_AudioSource != null)
        {
            e_AudioSource.PlayOneShot(EnemyDeathSound);
        }
    }

    void PlayEnemyChaseSound()
    {
        if (e_AudioSource != null)
        {
            e_AudioSource.PlayOneShot(EnemyChaseSound);
        }
    }

    private void OnDisable()
    {
        var enemyDamagedController = GetComponent<EnemyDamagedController>();
        if (enemyDamagedController != null)
        {
            enemyDamagedController.isDamaged -= PlayEnemyDamagedSound;
            enemyDamagedController.isDead -= PlayEnemyDeathSound;
        }

        var enemyChaseController = GetComponent<EnemyChaseController>();
        if (enemyChaseController != null)
        {
            enemyChaseController.PlayerChase -= PlayEnemyChaseSound;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource p_AudioSource;

    // Sounds
    [SerializeField] AudioClip AttackSound;
    [SerializeField] AudioClip TakeDamageSound;

    private void Start()
    {
        AttackController.Attack1 += PlayAttackSound;
        AttackController.Attack2 += PlayAttackSound;
        PlayerHealthandDamageController.isDamaged += PlayTakeDamageSound;
    }

    void PlayAttackSound()
    {
        if (p_AudioSource != null)
        {
            p_AudioSource.PlayOneShot(AttackSound);
        }
    }

    void PlayTakeDamageSound()
    {
        if (p_AudioSource != null)
        {
            p_AudioSource.PlayOneShot(TakeDamageSound);
        }
    }

    private void OnDisable()
    {
        AttackController.Attack1 -= PlayAttackSound;
        AttackController.Attack2 -= PlayAttackSound;
        PlayerHealthandDamageController.isDamaged -= PlayTakeDamageSound;
    }
}

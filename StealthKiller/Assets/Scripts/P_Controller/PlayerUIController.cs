using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image HealthBarBG;

    [SerializeField] private float Health;
    [SerializeField] private float maxHealth;

    [SerializeField] private PlayerHealthandDamageController playerHealthandDamageController; 

    private void Start()
    {
        maxHealth = 100f;
    }

    private void Update()
    {
        HealthBarUpdated();
    }

    void HealthBarUpdated()
    {
        Health = playerHealthandDamageController.health;

        HealthBar.fillAmount = Health / maxHealth;
    }
}

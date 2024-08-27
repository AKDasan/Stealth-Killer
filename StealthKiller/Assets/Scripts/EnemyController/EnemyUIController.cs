using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Image ChaseLimitBar;
    [SerializeField] private Image ChaseLimitBG;

    [SerializeField] private float ChaseLimit;
    [SerializeField] private float maxChaseLimit;

    [SerializeField] private Vector3 offset;

    private Camera cam;

    [SerializeField] private EnemyChaseController enemyChaseController;

    private bool isDead = false;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        maxChaseLimit = 10f;

        var enemyDamagedController = GetComponent<EnemyDamagedController>();
        if (enemyDamagedController != null)
        {
            enemyDamagedController.isDead += UIDisabled;
        }
    }

    private void LateUpdate()
    {
        ChaseLimitBar.transform.position = transform.position + offset;
        ChaseLimitBG.transform.position = transform.position + offset;

        ChaseLimitBar.transform.rotation = cam.transform.rotation;
        ChaseLimitBG.transform.rotation = cam.transform.rotation;
    }

    private void Update()
    {
        ChaseLimitIconController();
    }

    void ChaseLimitIconController()
    {
        ChaseLimit = enemyChaseController.chaseLimit;

        if (ChaseLimit < 0 || isDead) 
        {
            ChaseLimitBar.enabled = false;
            ChaseLimitBG.enabled = false;
        }
        else
        {
            ChaseLimitBar.enabled = true;
            ChaseLimitBG.enabled = true;
        }

        ChaseLimitBar.fillAmount = ChaseLimit / maxChaseLimit;
    }

    void UIDisabled()
    {
        isDead = true;
    }

    private void OnDisable()
    {
        var enemyDamagedController = GetComponent<EnemyDamagedController>();
        if (enemyDamagedController != null)
        {
            enemyDamagedController.isDead -= UIDisabled;
        }
    }
}

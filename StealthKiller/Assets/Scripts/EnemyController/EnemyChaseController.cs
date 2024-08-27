using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseController : MonoBehaviour
{
    private float coneAngle = 45f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private int rayCount = 10;
    [SerializeField] private int rayOffset = 5;

    [SerializeField] private float ChaseLimit = 0f;

    public event Action PlayerChase;
    public event Action NotChase;
    [SerializeField] private bool isLook = false;
    [SerializeField] private bool isChaseAfter = false;
    [SerializeField] private bool isManualChase = false;
    [SerializeField] private bool isChaseLimitUpdate = false;

    public float chaseLimit
    {
        get { return ChaseLimit; }
    }


    private void Start()
    {
        ChaseLimit = 0f;

        var enemyDamagedController = GetComponent<EnemyDamagedController>();
        if (enemyDamagedController != null)
        {
            enemyDamagedController.isDamaged += PlayerChaseManuelStart;
        }
    }

    void Update()
    {
        ChaseLimitController();
        ChaseConeRay();
    }

    void ChaseConeRay()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * rayOffset;

        bool playerDetected = false;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = coneAngle * (i / (float)(rayCount - 1)) - coneAngle / 2;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
            Ray ray = new Ray(rayOrigin, direction);

            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    playerDetected = true;
                    ChaseLimit += 5f * Time.deltaTime;
                    if (ChaseLimit >= 10 && !isLook && !isManualChase)
                    {
                        isLook = true;
                        isChaseAfter = true;
                        Debug.Log("Player takip ediliyor!");
                        PlayerChase?.Invoke();
                    }
                }
            }

            Debug.DrawRay(rayOrigin, direction * rayDistance, Color.red);
        }

        if (!playerDetected)
        {
            ChaseLimit -= 0.25f * Time.deltaTime;
        }
    }

    void ChaseLimitController()
    {
        if (ChaseLimit > 10)
        {
            ChaseLimit = 10;
        }
        if (ChaseLimit < 0)
        {
            ChaseLimit = 0;
            isLook = false;
            isManualChase = false;
            isChaseLimitUpdate = false;
            StartCoroutine(IsChaseAfterController());
        }
        if (!isLook && isChaseAfter)
        {
            NotChase?.Invoke();
            isChaseAfter = false;
        }
    }

    IEnumerator IsChaseAfterController()
    {
        yield return new WaitForSeconds(0.4f);
        isChaseAfter = false;
    }

    void PlayerChaseManuelStart()
    {
        isManualChase = true;
        if (!isLook && ChaseLimit < 8f) 
        {
            PlayerChase?.Invoke();
            if (!isChaseLimitUpdate)
            {
                ManualChaseLimitUpdate();
            }
        } 
    }

    void ManualChaseLimitUpdate()
    {
        isChaseLimitUpdate = true;
        ChaseLimit += 10f;
    }

    private void OnDisable()
    {
        var enemyDamagedController = GetComponent<EnemyDamagedController>();
        if (enemyDamagedController != null)
        {
            enemyDamagedController.isDamaged -= PlayerChaseManuelStart;
        }
    }
}

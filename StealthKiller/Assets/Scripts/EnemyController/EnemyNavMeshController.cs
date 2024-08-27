using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform AfterAttackTrackPoint;

    [SerializeField] private bool isChaseActive = false;
    [SerializeField] private bool isAttacking = false;

    //Coroutine control
    [SerializeField] private bool isHandleCoroutineRun = false;
    [SerializeField] private bool isAttackCycleFinish = true;

    [SerializeField] private float attackDistance = 2f;

    public event Action isChasingRun;
    public event Action isAttackDistance;

    private void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Player = playerObject.transform;
        }

        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Start()
    {
        var enemyChaseController = GetComponent<EnemyChaseController>();
        if (enemyChaseController != null)
        {
            enemyChaseController.PlayerChase += isChaseActiveController;
            enemyChaseController.NotChase += isChaseInActiveController;
        }

        var enemyDamagedController = GetComponent<EnemyDamagedController>();
        if (enemyDamagedController != null)
        {
            enemyDamagedController.isDead += isChaseInActiveController;
        }
    }

    private void Update()
    {
        if (isChaseActive && !isAttacking) // isAttacking kontrolü eklendi
        {
            Debug.Log("Player farkedildi!");
            StartCoroutine(ChasePlayer());

            if (IsPlayerInRange() && !isHandleCoroutineRun && isAttackCycleFinish)
            {
                StartCoroutine(HandlePlayerInRange());
            }
        }
        else
        {
            ChaseStop();
        }
    }

    void isChaseActiveController()
    {
        isChaseActive = true;
    }

    void isChaseInActiveController()
    {
        isChaseActive = false;
    }

    Vector3 UpdatePlayerPosition(Transform player)
    {
        Vector3 position = player.transform.position;
        return position;
    }

    Vector3 UpdateAfterAttackPosition(Transform player)
    {
        Vector3 direction = (transform.position - player.transform.position);
        Vector3 position = transform.position + direction * 2f;
        return position;
    }

    IEnumerator ChasePlayer()
    {
        agent.isStopped = false;
        yield return new WaitForSeconds(1.5f); // StandUp animasyonunu bitirme süresi
        if (!isAttacking && isAttackCycleFinish)
        {
            agent.speed = 3f;
            agent.SetDestination(UpdatePlayerPosition(Player));
        }
        else
        {
            agent.speed = 3.5f;
            agent.SetDestination(UpdateAfterAttackPosition(Player));
        }

        if (!IsPlayerInRange() && !isAttacking)
        {
            isChasingRun?.Invoke();
        }
    }

    void ChaseStop()
    {
        agent.isStopped = true;
    }

    bool IsPlayerInRange()
    {
        if (Player != null)
        {
            float distance = Vector3.Distance(transform.position, Player.position);
            return distance <= attackDistance;
        }
        return false;
    }

    IEnumerator HandlePlayerInRange()
    {
        isHandleCoroutineRun = true;
        agent.isStopped = true;
        isAttacking = true;
        isAttackDistance?.Invoke();
        yield return new WaitForSeconds(0.85f); // Attack animasyon süresi
        StartCoroutine(HandleAttackCycle());
        isAttacking = false;
        agent.isStopped = false;
        isHandleCoroutineRun = false;
    }

    IEnumerator HandleAttackCycle()
    {
        isAttackCycleFinish = false;
        yield return new WaitForSeconds(5f);
        isAttackCycleFinish = true;
    }

    private void OnDisable()
    {
        var enemyChaseController = GetComponent<EnemyChaseController>();
        if (enemyChaseController != null)
        {
            enemyChaseController.PlayerChase -= isChaseActiveController;
            enemyChaseController.NotChase -= isChaseInActiveController;
        }

        var enemyDamagedController = GetComponent<EnemyDamagedController>();
        if (enemyDamagedController != null)
        {
            enemyDamagedController.isDead -= isChaseInActiveController;
        }
    }
}

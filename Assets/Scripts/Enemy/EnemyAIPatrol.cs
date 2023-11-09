using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIPatrol : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, PlayerLayer;

    // Tuần tra
    Vector3 destinationPoint;
    bool walkpointSet;
    [SerializeField] float range;

    // Thay đổi trạng thái
    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttackRange;
    [SerializeField] private HealthBar healthBar;

    #region Variables: Health

    [SerializeField] private float maxHealth, currentHealth;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");

        // Máu
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, PlayerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, PlayerLayer);

        if (!playerInSight && !playerInAttackRange)
        {
            Patrol();
        }
        if (playerInSight && !playerInAttackRange)
        {
            Chase();
        }
        if (playerInSight && playerInAttackRange)
        {
            Attack();
        }
    }

    void Chase() // Hàm truy đuổi người chơi
    {
        agent.SetDestination(player.transform.position);
    }

    void Attack()
    {
        // Physics.OverlapBox
    }

    void Patrol() // Hàm đi tuần tra
    {
        if (!walkpointSet) SearchForDestination(); // Tìm kiếm điểm đến mới nếu điểm = false
        if (walkpointSet)
        {
            agent.SetDestination(destinationPoint);
        }
        if (Vector3.Distance(transform.position, destinationPoint) < 10)
        {
            walkpointSet = false;
        }
    }

    void SearchForDestination() // Tìm kiếm ngẫu nhiên vị trí đi tuần
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destinationPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destinationPoint, Vector3.down, groundLayer))
        {
            walkpointSet = true;
        }
    }
}

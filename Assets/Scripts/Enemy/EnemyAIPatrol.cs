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

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (!walkpointSet) SearchForDestination();
        if (walkpointSet)
        {
            agent.SetDestination(destinationPoint);
        }
        if (Vector3.Distance(transform.position, destinationPoint) < 10)
        {
            walkpointSet = false;
        }
    }

    void SearchForDestination()
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

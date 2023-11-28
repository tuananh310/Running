using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleBullet : MonoBehaviour
{
    #region Singleton

    public static SimpleBullet instance;
    [SerializeField] private float damage = 20f;

    #endregion

    NavMeshAgent _navMeshBullet;
    Transform target;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private float destroyTime;
    // private GameObject gameObject;

    private void Start()
    {
        _navMeshBullet = GetComponent<NavMeshAgent>();
        target = PlayerController.instance.targetEnemy;
    }
    void Update()
    {
        if (target != null)
        {
            _navMeshBullet.SetDestination(target.position);
        }

        Destroy(transform.gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag != "Player")
        {
            Debug.Log(other.tag);
            Destroy(transform.gameObject);
            if (other.tag == "Enemy")
            {
                var enemy = other.transform.gameObject.GetComponent<EnemyAIPatrol>();
                enemy.currentHealth -= damage;
                enemy.healthBar.SetHealth(enemy.currentHealth);
                if (enemy.currentHealth <= 0)
                {
                    Destroy(other.transform.gameObject);
                }
            }
        }
    }
}

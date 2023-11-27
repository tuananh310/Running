using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    #region Singleton

    public static SimpleBullet instance;
    [SerializeField] private float damage = 20f;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField] private float destroyTime;
    // private GameObject gameObject;

    // private void Start() {
    //     gameObject = 
    // }
    void Update()
    {
        Destroy(transform.gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
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

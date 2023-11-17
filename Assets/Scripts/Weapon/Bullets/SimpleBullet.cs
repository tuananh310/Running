using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    #region Singleton

    public static SimpleBullet instance;

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
        }
        // Debug.Log(other.transform.gameObject);
    }
}

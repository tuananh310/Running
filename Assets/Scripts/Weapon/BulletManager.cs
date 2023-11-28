using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletManager : MonoBehaviour
{
    #region Singleton

    public static BulletManager instance;

    #endregion

    [SerializeField] private GameObject simpleBullet, gun;

    [SerializeField] private float bulletSpeed;

    private void Awake()
    {
        instance = this;
    }

    public void Shooting(Vector3 initPosition)
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var bullet = Instantiate(simpleBullet, initPosition, Quaternion.identity);
            gun.transform.LookAt(hit.point);
            // bullet.transform.rotation = Quaternion.LookRotation(hit.point);
            // bullet.transform.LookAt(hit.point);
            // var bullet = Instantiate(simpleBullet, initPosition, gun.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = gun.transform.forward * bulletSpeed;
            // bullet.GetComponent<NavMeshAgent>().SetDestination(target.position);
        }

    }
}

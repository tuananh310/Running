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

    public void Shooting()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            PlayerController.instance.transform.LookAt(hit.point);
            var bullet = Instantiate(simpleBullet, gun.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = gun.transform.forward * bulletSpeed;
        }

    }
}

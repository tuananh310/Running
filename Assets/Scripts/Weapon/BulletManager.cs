using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    #region Singleton

    public static BulletManager instance;

    private void Awake() {
        instance = this;
    }

    #endregion
    [SerializeField] private GameObject simpleBullet, gun;

    [SerializeField] private float bulletSpeed;

    public void Shooting(Vector3 initPosition)
    {
        var bullet = Instantiate(simpleBullet, initPosition, gun.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = gun.transform.forward * bulletSpeed;
    }
}

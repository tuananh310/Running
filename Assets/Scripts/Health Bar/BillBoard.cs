using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;

    private void LateUpdate() {
        transform.LookAt(transform.position + mainCamera.forward);
    }
}

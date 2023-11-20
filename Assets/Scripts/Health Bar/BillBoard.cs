using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Transform mainCamera;

    private void Start() {
        mainCamera = Camera.main.transform;
    }

    private void LateUpdate() {
        transform.LookAt(transform.position + mainCamera.forward);
    }
}

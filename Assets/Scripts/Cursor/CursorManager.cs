using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    #region Singleton

    public static CursorManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    public Vector2 normalCursorHotSpot;
    public Vector2 attackCursorHotSpot;
    public Texture2D normalCursor;
    public Texture2D attackCursor;

    private void Update()
    {
        DetectTarget();
    }

    private void DetectTarget()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            String layerName = LayerMask.LayerToName(hit.transform.gameObject.layer);
            switch (layerName)
            {
                case "Enemy":
                    Cursor.SetCursor(attackCursor, attackCursorHotSpot, CursorMode.Auto);
                    if (Input.GetMouseButton(1))
                    {
                        PlayerController.instance.targetEnemy = hit.transform;
                        // PlayerController.instance.Attack(hit.transform);
                    }

                    break;
                default:
                    Cursor.SetCursor(normalCursor, normalCursorHotSpot, CursorMode.Auto);
                    break;
            }
        }
    }
}

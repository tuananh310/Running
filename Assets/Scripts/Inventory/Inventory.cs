using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInventory(InputAction.CallbackContext context){
        if(context.started){
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}

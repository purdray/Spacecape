using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskActivator : MonoBehaviour
{
    public GameObject taskObject;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            Debug.Log("Entered Task");
           taskObject.SetActive(true);
           Cursor.lockState = CursorLockMode.None;
        }
        
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            Debug.Log("Exited Task");
            taskObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }



}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class oxygen : MonoBehaviour
{
    public GameObject oxySound;



public Collider col;





    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other) {
        


        if(other.CompareTag("Player")){
            Debug.Log("Entered Oxygen");
            //StartCoroutine(fill()); 
            oxygenBar.timerRunning = false;
            oxySound.SetActive(true);

        }

    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            Debug.Log("Exited Oxygen");
            oxygenBar.timerRunning = true;
            oxySound.SetActive(false);
        }
    }



    // Update is called once per frame
    void Update()
    {
  
      
    }
   /* public IEnumerator fill()
    {
        oxygenBar.timerRunning = false;
        yield return new WaitForSeconds(3f);
        oxygenBar.timerRunning = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }*/

}


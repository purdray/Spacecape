using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterOpenDoor : MonoBehaviour
{
    public GameObject doorSound;

    public GameObject leftDoor;
    public GameObject rightDoor;
    
    Animator leftAnim;
    Animator rightAnim;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            Debug.Log("Entered Collider");
            leftAnim = leftDoor.GetComponent<Animator>();
            rightAnim = rightDoor.GetComponent<Animator>();
            leftAnim.Play("LeftOpen");
            rightAnim.Play("rightOpen");
            doorSound.SetActive(true);

        }
        
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            Debug.Log("Exited Collider");
            leftAnim = leftDoor.GetComponent<Animator>();
            rightAnim = rightDoor.GetComponent<Animator>();
            leftAnim.Play("leftClose");
            rightAnim.Play("rightClose");
            doorSound.SetActive(false);
            doorSound.SetActive(true);

        }

    }
}

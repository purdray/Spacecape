using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            Debug.Log("Entered Deathzone");
            SceneManager.LoadScene("Death");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
}
}

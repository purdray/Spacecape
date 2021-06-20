using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour
{


    public Material material;
    public GameObject bildschirm;
    public GameObject alarmSound;

    public GameObject effects;
    public GameObject generatorSound;
    
    // Start is called before the first frame update
    void Start()
    {
        alarmSound.SetActive(true);    
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            Debug.Log("Entered Task");
            bildschirm.GetComponent<MeshRenderer>().material = material;
            oxygenBar.timerRunning = false;
            alarmSound.SetActive(false);
            StartCoroutine(delay());
            effects.SetActive(false);
            generatorSound.SetActive(true);
        }
    }

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("winscene");
        Cursor.lockState = CursorLockMode.None;
    }
}

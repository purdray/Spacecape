using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    
    
        private const float Seconds = 42f;

    // Start is called before the first frame update
    void Start()
    { 
    StartCoroutine(delay());     
      
    }

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(Seconds);
        SceneManager.LoadScene("lvl1");
    }
    public void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            print("space key was pressed");
            SceneManager.LoadScene("lvl1");
            StopCoroutine(delay());
        } 
        
    }


}

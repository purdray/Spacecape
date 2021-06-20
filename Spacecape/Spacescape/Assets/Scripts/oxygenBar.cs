using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class oxygenBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider oxygenSlider;
    public Gradient gradient;
    public Image fillBar;
    public float timerStart = 120; // timer
    public float timer;

    public static bool timerRunning = true;


    // Start is called before the first frame update
    void Start()
    {
        oxygenSlider.maxValue = timerStart;
        timer = timerStart;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timerRunning){
            timer -= Time.deltaTime;
        }else{
            if(timer <= timerStart){
                timer += Time.deltaTime * 4;
            }

        }
        this.fillBar.color = gradient.Evaluate(oxygenSlider.normalizedValue);
        this.oxygenSlider.value = timer;
        if(timer <= 0){
            SceneManager.LoadScene("Death");
            Cursor.lockState = CursorLockMode.None;
        }

    }
}

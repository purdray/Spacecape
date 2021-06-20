using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeBar : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient gradient;
    public Image fillBar;
    public float maxHealth = 100;
    public float currentLife = 100;
    void Start()
    {
        
        healthSlider.maxValue = maxHealth;

        
    }

    // Update is called once per frame
    void Update()
    {
        currentLife = playerMovement.playerHealth;
        healthSlider.value = currentLife;
        this.fillBar.color = gradient.Evaluate(healthSlider.normalizedValue);
        if(currentLife <= 0){
            SceneManager.LoadScene("Death");
            Cursor.lockState = CursorLockMode.None;
        }

        
    }
}

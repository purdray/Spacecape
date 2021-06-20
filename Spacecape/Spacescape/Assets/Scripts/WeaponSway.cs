using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float intensity = 1.5f;
    public float smooth = 10f;
    private Quaternion initrotation;

    // Start is called before the first frame update
    void Start()
    {
        // Default rotation der Waffe setzen
        initrotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSway();
    }
    private void UpdateSway(){
        // Achsen
        float x_mouse = Input.GetAxis("Mouse X");
        float y_mouse = Input.GetAxis("Mouse Y");

        // Target Rotation ändern in X Richtung
        Quaternion xAdjustment = Quaternion.AngleAxis(-intensity * x_mouse,Vector3.up);   
        Quaternion yAdjustment = Quaternion.AngleAxis(intensity * y_mouse,Vector3.right); 
        Quaternion targetRotation = initrotation * xAdjustment * yAdjustment;  
        // Wieder zurück rotieren
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation,Time.deltaTime * smooth);  

    }
}

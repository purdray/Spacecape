using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float speed =150f;
    public Transform playerBody;
    public Transform weapon;
    


    float yRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //Mauskursor auf die Mitte des Bildchirmes fixieren
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {

        // Achsen
        float mouseX = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);  // Rotation für y Achse begrenzen auf 180 Grad

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);  // Auf- und Abschauen mit der Kamera
       

        
        

        playerBody.Rotate(Vector3.up * mouseX); // Links und rechts schauen mit dem Körper
    }
}

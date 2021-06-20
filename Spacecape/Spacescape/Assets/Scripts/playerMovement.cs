using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{

    public CharacterController controller;

    public static float initSpeed = 6;
    public static float initEndurance = 100;
    public static float sprintMod = 1.25f;
    public static float inWaterMod = 0.5f;

    // Ausdauer Regeneration-Timer
    public float enduranceRegenTimer = 0.0f;
    public float enduranceTimeToRegen = 3.0f;

    public float resetEnduranceTimer = 0.0f;

    // Filed of View
    private float baseFOV = 0;
    public float fovMod = 1.5f;

    public float playerHeight = 2f;

    public float gravity = -35f;
    public float jumpHeight = 2f;
    public Camera playerCam;

    public static float speed;
    public static float playerHealth = 100;
    public static float endurance;
    private float movementCounter;
    private float idleCounter;

    Vector3 velocity;
    public Vector3 targetWeaponBobPosition;
    private Vector3 weaponParentOrigin;
    public Transform weaponParent;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public GameObject windSound;


    //Game objects
    public GameObject Taskobject;
    public GameObject chooseWeapon;




    // Start is called before the first frame update
    void Start()
    {
        baseFOV = playerCam.fieldOfView;
        speed = initSpeed;

        endurance = initEndurance;
        weaponParentOrigin = weaponParent.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Achsen
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Eingaben
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isJumping = Input.GetButtonDown("Jump");
        bool crouching = Input.GetKey(KeyCode.LeftControl);


        // Ducken
        if (crouching && controller.isGrounded)
        {
            controller.height = 1.0f;
            speed = initSpeed * inWaterMod;
        }
        else
        {
            controller.height = playerHeight;
            speed = initSpeed;
        }

        // Springen
        if (isJumping && controller.isGrounded && endurance > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            endurance -= 10f;
            enduranceRegenTimer = resetEnduranceTimer;
        }


        // Sprinten
        if (isSprinting && endurance > 0 && z > 0 && !crouching)
        {
            speed = initSpeed * sprintMod;
            updateEndurance(-20f);
            enduranceRegenTimer = resetEnduranceTimer;

            //FOV Änderung
            playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, baseFOV * fovMod, Time.deltaTime * 8f);   //"Lerp" = vom jetzigen FOV zum gewünschtem FOV in bestimmter Zeit 
        }
        // Ausdauer erhöht sich erst nachdem man 3 Sekunden lang nicht sprintet
        else if (endurance < initEndurance)
        {
            if (enduranceRegenTimer >= enduranceTimeToRegen)
            {
                updateEndurance(10f);
            }
            else
            {
                enduranceRegenTimer += Time.deltaTime;
            }
            playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, baseFOV, Time.deltaTime * 4f);
        }


        // Laufen mit angepasster Geschwindigkeit
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Auswirkung von Gravity und Sprung
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        // Atmen beim Stehen
        if (x == 0 && z == 0 || crouching)
        {
            headBob(idleCounter, 8f, 8f);
            idleCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
            // Headbob beim Laufen
        }
        else if (!isSprinting)
        {
            headBob(movementCounter, 12f, 10f);
            movementCounter += Time.deltaTime * 6f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6f);
            //Headbob beim Sprinten 
        }
        else if (isSprinting && endurance > 0 && z > 0 && !crouching)
        {
            headBob(movementCounter, 16f, 12f);
            movementCounter += Time.deltaTime * 10f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);
        }
        else if (z == 0 && Input.GetMouseButtonDown(1))
        {
            headBob(movementCounter, 0.1f, 0.1f);
            movementCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
        }
        else
        {
            headBob(movementCounter, 12f, 10f);
            movementCounter += Time.deltaTime * 6f;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6f);
        }


    }




    // Langsammer wenn man im Wasser ist (nützlich für spätere Tasks im Wasser)
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("projectile"))
        {
            playerHealth -= 10;
        }
        if (other.CompareTag("windOff"))
        {
            windSound.SetActive(false);
        }
        
    }
    // Schneller sobald man aus dem Wasser ist
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("windOn"))
        {
            windSound.SetActive(true);
            chooseWeapon.SetActive(true);
            Destroy(chooseWeapon,5f);
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("fire"))
        {
            playerHealth -= 5 * Time.deltaTime;
        }
        
    }

    private void updateEndurance(float inc)
    {
        endurance += inc * Time.deltaTime;
    }

    // Übergebene werte werden auf eine Sinus und Kosinus-Kurve übergeben, da wir nicht einfach auf und ab atmen/bewegen wollen 
    void headBob(float z, float xIntensity, float yIntensity)
    {
        targetWeaponBobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(z) * xIntensity, Mathf.Sin(z * 2) * yIntensity, 0);
    }


}

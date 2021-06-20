using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class TurretBody : MonoBehaviour
{
    private float initHealth = 4f;
    private static float currentHealth1;
    private static float currentHealth2;
    public static int turretsAlive = 2;

    public static Animation anim;
    public GameObject turretShoot;



    public GameObject t1;
    public GameObject t2;

    public static bool isActive = true;
    public static bool isActive2 = true;
    public static bool turretDestroyed1 = false;
    public static bool turretDestroyed2 = false;
    public static GameObject turret1;
    public static GameObject turret2;

    // Effects
    public static ParticleSystem explosion;
    public ParticleSystem exp;
    public ParticleSystem sm;
    public static ParticleSystem smoke;
    public AudioClip boom;
    public AudioClip rauch;
    public static AudioClip boomSound;
    public static AudioClip rauchSound;






    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        turret1 = t1;
        turret2 = t2;
        smoke = sm;
        explosion = exp;
        boomSound = boom;
        rauchSound = rauch;
        currentHealth1 = initHealth;
        currentHealth2 = initHealth;
        anim = GetComponent<Animation>();
        anim.Play("TurretIdle");


    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(new Vector3(2 * transform.position.x - target.position.x, transform.position.y, 2 * transform.position.z - target.position.z));
        // transform.rotation = Quaternion.LookRotation(new Vector3(transform.position.x - target.position.x, transform.position.y, transform.position.z - target.position.z));
    }
    
    public IEnumerator ChangeToScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Death");
        Cursor.lockState = CursorLockMode.None;
    }
    public static void looseLife(string tag)
    {
        switch (tag)
        {
            case "Turret1":
                currentHealth1--;
                Debug.Log("Turret1 got hit" + currentHealth1);
                break;
            case "Turret2":
                currentHealth2--;
                Debug.Log("Turret2 got hit" + currentHealth2);
                break;
        }
        if (currentHealth1 == 0)
        {
            if (!turretDestroyed1)
            {
                turret1.GetComponent<Animation>().Play("TurretSleep");
                isActive = false;
                Instantiate(explosion, turret1.transform.position, Quaternion.identity);
                Instantiate(smoke, turret1.transform.position, Quaternion.Euler(-90f,0f,0f));
                AudioSource.PlayClipAtPoint(boomSound,turret1.transform.position,10f);
                AudioSource.PlayClipAtPoint(rauchSound,turret1.transform.position,0.5f);
                turretDestroyed1 = true;
                turretsAlive--;
            }


        }
        if (currentHealth2 == 0)
        {
            if (!turretDestroyed2)
            {
                turret2.GetComponent<Animation>().Play("TurretSleep");
                isActive2 = false;
                Instantiate(explosion, turret2.transform.position, Quaternion.identity);
                Instantiate(smoke, turret2.transform.position, Quaternion.Euler(-90f,0f,0f));
                AudioSource.PlayClipAtPoint(boomSound,turret2.transform.position,10f);
                AudioSource.PlayClipAtPoint(rauchSound,turret2.transform.position,0.5f);
                turretDestroyed2 = true;
                turretsAlive--;
            }


        }
    }

}

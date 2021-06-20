using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretActivator : MonoBehaviour
{
    public Animation anim1;
    public Animation anim2;
    public static bool inRange = false;
    public static bool turretsAlive = true;
    public Transform head1;
    public Transform head2;
    public GameObject turret1;
    public GameObject turret2;

    public Rigidbody projectile;
    //public GameObject projectile;
    public GameObject projectileSpawnPoint1;
    public GameObject projectileSpawnPoint2;
    public float speed;
    public bool canShoot1;
    public bool canShoot2;


    public Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && turretsAlive)
        {
            Debug.Log("Entered Turret Activation");
            anim1.Play("legacy");
            anim2.Play("legacy");
            inRange = true;
            StartCoroutine(animationWait());
            

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && turretsAlive)
        {
            Debug.Log("Exited Turret Activation");
            anim1.Play("TurretSleep");
            anim2.Play("TurretSleep");
            inRange = false;
            canShoot1 = false;
            canShoot2 = false;

        }
    }

    void Update()
    {
        if (inRange)
        {
            if (TurretBody.isActive)
            {
                //head1
                head1.transform.LookAt(new Vector3(2 * head1.transform.position.x - target.position.x, 2 * head1.transform.position.y - target.position.y, 2 * head1.transform.position.z - target.position.z));
                head1.transform.Rotate(new Vector3(head1.transform.rotation.x + 90, head1.transform.rotation.y, head1.transform.rotation.z));
                //turret1
                turret1.transform.LookAt(new Vector3(2 * turret1.transform.position.x - target.position.x, turret1.transform.position.y, 2 * turret1.transform.position.z - target.position.z));
                
                if(canShoot1){
                    Rigidbody projectileClone = Instantiate(projectile, projectileSpawnPoint1.transform.position,Quaternion.identity) as Rigidbody;
                    projectileClone.rotation = projectileSpawnPoint1.transform.rotation;
                    projectileClone.useGravity = false;
                    projectileClone.AddForce(projectileSpawnPoint1.transform.forward * speed * Time.deltaTime);
                    Debug.Log("turret1 shoot");
                    canShoot1 = false;
                    StartCoroutine(reload1());
                    Destroy(projectileClone,10f);
                }
            }
            if (TurretBody.isActive2)
            {
                //head2
                head2.transform.LookAt(new Vector3(2 * head2.transform.position.x - target.position.x, 2 * head2.transform.position.y - target.position.y, 2 * head2.transform.position.z - target.position.z));
                head2.transform.Rotate(new Vector3(head2.transform.rotation.x + 90, head2.transform.rotation.y, head2.transform.rotation.z));
                //turret2
                turret2.transform.LookAt(new Vector3(2 * turret2.transform.position.x - target.position.x, turret2.transform.position.y, 2 * turret2.transform.position.z - target.position.z));
                if(canShoot2){
                    Rigidbody projectileClone2 = Instantiate(projectile, projectileSpawnPoint2.transform.position,Quaternion.identity) as Rigidbody;
                    projectileClone2.rotation = projectileSpawnPoint2.transform.rotation;
                    projectileClone2.useGravity = false;
                    projectileClone2.AddForce(projectileSpawnPoint2.transform.forward * speed * Time.deltaTime);
                    Debug.Log("turret2 shoot");
                    canShoot2 = false;
                    StartCoroutine(reload2());
                    Destroy(projectileClone2,10f);

                }
            }


        }
    }
    public IEnumerator animationWait()
    {
        yield return new WaitForSeconds(3); 
        canShoot1 = true;
        canShoot2 = true;
    }
    public IEnumerator reload1()
    {
        yield return new WaitForSeconds(1);
        canShoot1 = true;
        
    }
        public IEnumerator reload2()
    {
        yield return new WaitForSeconds(1);
        canShoot2 = true;
        
    }

}

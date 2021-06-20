using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Guns[] loadout;
    public Transform weaponParent;
    public GameObject bulletHolePrefab;
    public LayerMask canBeShot;
    public LayerMask turret;
    public AudioSource soundWeapon;
    public AudioClip impactHit;
    public float currentCooldown;
    public int currentId;
    private GameObject currentEquipment;




    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        // Wenn nummer 1 gedrückt wird, soll die erste Waffe ausgewählt werden.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Equip(0);
        }
        if (currentEquipment != null)
        {
            Aim(Input.GetMouseButton(1));

            if (Input.GetMouseButtonDown(0) && currentCooldown <= 0)
            {
                Shoot();
            }

            // Waffenposition nach dem Schuss wieder zur originalen Position zurück
            currentEquipment.transform.localPosition = Vector3.Lerp(currentEquipment.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);

            // Cooldown
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
            }
        }





    }

    void Equip(int id)
    {
        // Wenn die gleiche Waffe schon Equipt ist, soll sie nicht nochmal geklont werden
        if (currentEquipment != null)
        {
            Destroy(currentEquipment);
        }
        currentId = id;
        GameObject newEquipment = Instantiate(loadout[id].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        newEquipment.transform.localPosition = Vector3.zero;
        newEquipment.transform.localEulerAngles = Vector3.zero;

        currentEquipment = newEquipment;
    }

    void Aim(bool isAiming)
    {
        Transform anchor = currentEquipment.transform.Find("Anchor");
        Transform stateADS = currentEquipment.transform.Find("States/ADS");
        Transform stateHip = currentEquipment.transform.Find("States/Hip");

        if (isAiming)
        {
            // Wenn Rechtsklick, dann wird geaimt
            anchor.position = Vector3.Lerp(anchor.position, stateADS.position, Time.deltaTime * loadout[currentId].aimSpeed);
        }
        else
        {
            // Wenn nicht, dann auf der Hüftposition
            anchor.position = Vector3.Lerp(anchor.position, stateHip.position, Time.deltaTime * loadout[currentId].aimSpeed);
        }

    }

    void Shoot()
    {
        Transform spawn = transform.Find("PlayerCamera");

        // bloom
        Vector3 weaponBloom = spawn.position + spawn.forward * 1000f;
        weaponBloom += Random.Range(-loadout[currentId].bloom, loadout[currentId].bloom) * spawn.up;
        weaponBloom += Random.Range(-loadout[currentId].bloom, loadout[currentId].bloom) * spawn.right;
        weaponBloom -= spawn.position;
        weaponBloom.Normalize();

        // Raycast
        RaycastHit hit = new RaycastHit();
        //hitting everything else
        if (Physics.Raycast(spawn.position, weaponBloom, out hit, 1000f, canBeShot))
        {
            GameObject newBulletHole = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.identity) as GameObject;
            newBulletHole.transform.LookAt(hit.point + hit.normal);
            AudioSource.PlayClipAtPoint(impactHit, hit.point);
            Destroy(newBulletHole, 5f);
        }
        //hitting a turret
        if (Physics.Raycast(spawn.position, weaponBloom, out hit, 1000f, turret))
        {
            GameObject newTurretBulletHole = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.identity) as GameObject;
            newTurretBulletHole.transform.LookAt(hit.point + hit.normal);
            AudioSource.PlayClipAtPoint(impactHit, hit.point);
            Destroy(newTurretBulletHole, 5f);
            if (hit.collider.gameObject.layer == 11)
            {
                TurretBody.looseLife(hit.collider.gameObject.tag);

            }
        }

        // Waffen Effekte
        currentEquipment.transform.Rotate(-loadout[currentId].recoil, 0, 0);
        currentEquipment.transform.position -= currentEquipment.transform.forward * loadout[currentId].kickback;

        // Cooldown = wie lange man warten muss, bis die Waffe wieder schießt
        currentCooldown = loadout[currentId].firerate;


        // Waffen Soundeffects
        soundWeapon.clip = loadout[currentId].gunShotSound;
        soundWeapon.pitch = 1 - loadout[currentId].pitchRandom + Random.Range(-loadout[currentId].pitchRandom, loadout[currentId].pitchRandom);
        soundWeapon.volume = loadout[currentId].gunVolume;
        soundWeapon.Play();
    }
}

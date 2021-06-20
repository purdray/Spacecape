using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun",menuName = "Gun")]
public class Guns : ScriptableObject
{
    public string gunName;
    public float firerate;
    public float recoil;
    public float kickback;
    public float bloom;
    public float aimSpeed = 10f;

    public AudioClip gunShotSound;
    public  float pitchRandom;
    public float gunVolume;

    public GameObject prefab;

}

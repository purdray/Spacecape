using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{

    public GameObject leftDoor;
    public GameObject rightDoor;
    
    Animator leftAnim;
    Animator rightAnim;

     // Start is called before the first frame update
    void Start()
    {
        leftAnim = leftDoor.GetComponent<Animator>();
        rightAnim = rightDoor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    
    }

}

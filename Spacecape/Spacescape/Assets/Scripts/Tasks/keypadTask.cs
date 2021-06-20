using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class keypadTask : MonoBehaviour {
   
   public Text inputCode;

public GameObject doors;

public GameObject doorSound;

    public GameObject leftDoor;
    public GameObject rightDoor;
    
    Animator leftAnim;
    Animator rightAnim;



   public int codeLength = 5;
   public float codeResetTimeInSeconds = 0.5f;
   private bool isResetting = false;
   private void OnEnable() {

      /*string code = string.Empty;
      
      for (int i = 0; i < codeLength; i++) {
         code += Random.Range(1, 10);
      }
     
      cardCode.text = code;*/
      inputCode.text = string.Empty;
   }
  
   public void ButtonClick(int number) {
      if (isResetting) { return; }
      
      inputCode.text += number;
      Debug.Log("number added");
      
      if (inputCode.text == ClipboardText.taskCode) {
         inputCode.text = "Correct";
         Debug.Log("Correct answer");
         StartCoroutine(ResetCode());
         Cursor.lockState = CursorLockMode.Locked;
         leftAnim = leftDoor.GetComponent<Animator>();
         rightAnim = rightDoor.GetComponent<Animator>();
         leftAnim.Play("LeftOpen");
         rightAnim.Play("rightOpen");
         doorSound.SetActive(true);
      }
      else if (inputCode.text.Length >= codeLength) {
         inputCode.text = "Failed";
         Debug.Log("Task Failed");  
         StartCoroutine(ResetCode());
      }
   }
   private IEnumerator ResetCode() {
      isResetting = true; 
      yield return new WaitForSeconds(codeResetTimeInSeconds);
      
     inputCode.text = string.Empty;
     isResetting = false;
   }
   void Update () {
      Cursor.visible = true;
   }
}
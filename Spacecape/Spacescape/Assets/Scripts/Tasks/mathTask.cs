using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class mathTask: MonoBehaviour {

   public Text inputCode;
   public Text cardCode;
   public Text cardLoesung;


   
   public GameObject doorSound;

   public GameObject leftDoor;
   public GameObject rightDoor;
   
    
   Animator leftAnim;
   Animator rightAnim;


   
   public float codeResetTimeInSeconds = 0.5f;
   private bool isResetting = false;
   public string[] tasks = {"2 * 6", "5 * 6 - 3", "27-13", "3 ^ 3", "55 - 68 + 14", "63 / 3", "52 / 4", "4!", "40 - 5 * 3", "35 + 76"}; // 10 Aufgaben
   public string[] solution = {"12", "27", "14", "27", "1", "21", "13", "24", "25", "111"}; // 10 Lösungen
   //public int[] solution = {12, 7, 14, 27, 0, 21, 13, 24, 25, 100};

   private void OnEnable() {

      string code = string.Empty;
      string loesung = string.Empty;
      
      //einen String zufällig auswählen
      int i = Random.Range(0,10);
      code = tasks[i];
      loesung = solution[i];

      cardLoesung.text = loesung;
      cardCode.text = code;
      inputCode.text = string.Empty;
   }
  
   public void ButtonClick(int number) {
      if (isResetting) { return; }
      
      inputCode.text += number;
      Debug.Log("number added");
      
      if (inputCode.text == cardLoesung.text) {
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
      else if (inputCode.text.Length >= cardLoesung.text.Length) {
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
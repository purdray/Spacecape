using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClipboardText : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro clipCode;
    public int codeLength = 5;
    public static string taskCode = "";
    public static bool trueCodeGenerated = false;

    void Start()
    {
        string code = string.Empty;
        for (int i = 0; i < codeLength; i++) {
            code += Random.Range(1, 10);
      }
     
      clipCode.text = code;
      taskCode = code;
      trueCodeGenerated = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class whiteBoardText : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro boardText;
    public string numbers;
    public bool boardTextGenerated = false;

    
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!boardTextGenerated && ClipboardText.trueCodeGenerated){
            numbers = "???" + ClipboardText.taskCode[3] + "?";
            boardText.text = numbers;
            boardTextGenerated = true;
        }
        
        
    }
}

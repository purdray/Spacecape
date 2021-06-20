using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClipboardTextFalse : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro clipCode;
    public int codeLength = 5;
    private bool wrongCodeGenerated = false;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (!wrongCodeGenerated && ClipboardText.trueCodeGenerated)
        {
            string code = string.Empty;

            for (int i = 0; i < codeLength; i++)
            {
                int rdm = Random.Range(1, 10);
                while(i == 3 && rdm.ToString().Equals(ClipboardText.taskCode[3])){
                    rdm = Random.Range(1,10);
                }
                code += rdm;
            }

            clipCode.text = code;
            wrongCodeGenerated = true;
        }
    }
}

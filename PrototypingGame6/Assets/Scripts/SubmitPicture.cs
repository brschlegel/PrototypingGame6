using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SubmitPicture : DrawingButtons
{
    public Text text;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                SubmitPicture submit = hit.collider.GetComponent<SubmitPicture>();
                if (submit != null)
                {
                    buttonGraying.GrayButtons(type, id);
                    byte[] arr = Drawing.Texture.EncodeToPNG();
                    if (text.text == "")
                    {
                        File.WriteAllBytes(Application.dataPath + "/" + Time.fixedTime + ".png", arr);
                    }
                    else
                    {
                        File.WriteAllBytes(Application.dataPath + "/" + text.text + ".png", arr);
                    }
                }
            }
        }
    }
}

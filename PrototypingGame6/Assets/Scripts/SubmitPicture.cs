using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SubmitPicture : MonoBehaviour
{

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
                    byte[] arr = Drawing.Texture.EncodeToPNG();
                    File.WriteAllBytes(Application.dataPath + "/CrappyDrawing.png", arr);
                    Debug.Log(Application.dataPath);
                }
            }
        }
    }
}

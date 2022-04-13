using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushSize : MonoBehaviour
{

    public int brushSize;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                BrushSize bSize = hit.collider.GetComponent<BrushSize>();
                if (bSize != null && bSize.brushSize == brushSize)
                {
                    Paint.SetBrushSize(brushSize);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : DrawingButtons
{

    [SerializeField]
    private Renderer selectedColorPreview;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Eraser eraser = hit.collider.GetComponent<Eraser>();
                if (eraser != null)
                {
                    ColorPicker.SelectedColor = Color.white;
                    ColorPicker.BaseColor = Color.white;
                    selectedColorPreview.material.color = Color.white;
                    Paint.isEraser = true;
                    Paint.action = Actions.Painting;
                    buttonGraying.GrayButtons(type, id);
                }
            }
        }
    }
}

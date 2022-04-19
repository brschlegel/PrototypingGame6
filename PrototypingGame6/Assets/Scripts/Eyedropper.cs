using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyedropper : DrawingButtons
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
                Eyedropper eyedropper = hit.collider.GetComponent<Eyedropper>();
                if (eyedropper != null)
                {
                    Paint.action = Actions.Eyedropping;
                    buttonGraying.GrayButtons(type, id);
                }
                else if(Paint.action == Actions.Eyedropping)
                {
                    Drawing drawing = hit.collider.GetComponent<Drawing>();
                    if(drawing != null)
                    {
                        Renderer rend = hit.transform.GetComponent<Renderer>();

                        Texture2D tex = rend.material.mainTexture as Texture2D;
                        Vector2 pixelUV = hit.textureCoord;
                        pixelUV.x *= tex.width;
                        pixelUV.y *= tex.height;
                        ColorPicker.SelectedColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                        ColorPicker.BaseColor = ColorPicker.SelectedColor;
                        selectedColorPreview.material.color = ColorPicker.SelectedColor;
                    }
                }
            }
        }
    }
}

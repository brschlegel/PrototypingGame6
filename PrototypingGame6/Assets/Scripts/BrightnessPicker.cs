using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessPicker : MonoBehaviour
{
    private bool clicking;
    [SerializeField]
    private Renderer selectedColorPreview;
    [SerializeField]
    private Renderer eraser;
    [SerializeField]
    private Paint paint;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                BrightnessPicker picker = hit.collider.GetComponent<BrightnessPicker>();
                if (picker != null)
                {
                    clicking = true;
                    eraser.material.color = Color.white;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            clicking = false;
        }
        if (clicking && Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                BrightnessPicker picker = hit.collider.GetComponent<BrightnessPicker>();
                if (picker != null)
                {
                    Renderer rend = hit.transform.GetComponent<Renderer>();

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;

                    //ColorPicker.SelectedColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                    Color pixel = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);
                    if(pixel.g > .5f)
                    {
                        float gPercentage = (pixel.g - .5f) / .5f;
                        Color color = ColorPicker.BaseColor;
                        ColorPicker.SelectedColor = new Color(color.r + ((1f - color.r) * gPercentage), color.g + ((1f - color.g) * gPercentage), color.b + ((1f - color.b) * gPercentage));
                        selectedColorPreview.material.color = ColorPicker.SelectedColor;
                        paint.ChangeHintColor();
                    }
                    else if(pixel.g < .5f)
                    {
                        float gPercentage = (.5f - pixel.g) / .5f;
                        Color color = ColorPicker.BaseColor;
                        ColorPicker.SelectedColor = new Color(color.r - (color.r * gPercentage), color.g - (color.g * gPercentage), color.b - (color.b * gPercentage));
                        selectedColorPreview.material.color = ColorPicker.SelectedColor;
                        paint.ChangeHintColor();
                    }
                    //selectedColorPreview.material.color = ColorPicker.SelectedColor;
                }
            }
        }
    }
}

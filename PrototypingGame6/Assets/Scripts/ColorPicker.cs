using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Some of the below code taken from or inspired by https://unity3d.college/2017/07/22/build-unity-multiplayer-drawing-game-using-unet-unity3d/
/// </summary>

public class ColorPicker : MonoBehaviour
{
    public static Color SelectedColor { get; set; }
    private bool clicking;

    [SerializeField]
    private Renderer selectedColorPreview;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                ColorPicker picker = hit.collider.GetComponent<ColorPicker>();
                if (picker != null)
                {
                    clicking = true;
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
                ColorPicker picker = hit.collider.GetComponent<ColorPicker>();
                if (picker != null)
                {
                    Renderer rend = hit.transform.GetComponent<Renderer>();

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;
                    SelectedColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);

                    selectedColorPreview.material.color = SelectedColor;
                    Debug.Log(SelectedColor);
                }
            }
        }
    }
}

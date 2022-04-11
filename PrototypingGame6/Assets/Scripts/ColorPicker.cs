using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Much of the below code taken from or inspired by https://unity3d.college/2017/07/22/build-unity-multiplayer-drawing-game-using-unet-unity3d/
/// </summary>

public class ColorPicker : MonoBehaviour
{
    public static Color SelectedColor { get; set; }

    [SerializeField]
    private Renderer selectedColorPreview;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                ColorPicker picker = hit.collider.GetComponent<ColorPicker>();
                if (picker != null)
                {
                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    MeshCollider meshCollider = hit.collider as MeshCollider;

                    if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                        return;

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;
                    SelectedColor = tex.GetPixel((int)pixelUV.x, (int)pixelUV.y);

                    selectedColorPreview.material.color = SelectedColor;
                }
            }
        }
    }
}

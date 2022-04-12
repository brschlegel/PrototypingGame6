using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Much of the below code taken from or inspired by https://unity3d.college/2017/07/22/build-unity-multiplayer-drawing-game-using-unet-unity3d/
/// </summary>

public class Paint : MonoBehaviour
{
    public static int brushSize;
    private void Start()
    {
        brushSize = 10;
        //var data = Drawing.GetAllTextureData();
        //var zippeddata = data.Compress();

        //RpcSendFullTexture(zippeddata);
    }

    //private void RpcSendFullTexture(byte[] textureData)
    //{
    //    Drawing.SetAllTextureData(textureData.Decompress());
    //}

    private void Update()
    {
        Debug.Log(brushSize);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Drawing drawing = hit.collider.GetComponent<Drawing>();
                if (drawing != null)
                {
                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    MeshCollider meshCollider = hit.collider as MeshCollider;

                    if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                        return;

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;

                    //CmdBrushAreaWithColorOnServer(pixelUV, ColorPicker.SelectedColor, 1f);
                    BrushAreaWithColor(pixelUV, ColorPicker.SelectedColor, brushSize);
                }
            }
        }
    }

    //private void CmdBrushAreaWithColorOnServer(Vector2 pixelUV, Color color, int size)
    //{
    //    RpcBrushAreaWithColorOnClients(pixelUV, color, size);
    //    BrushAreaWithColor(pixelUV, color, size);
    //}

    //private void RpcBrushAreaWithColorOnClients(Vector2 pixelUV, Color color, int size)
    //{
    //    BrushAreaWithColor(pixelUV, color, size);
    //}

    private void BrushAreaWithColor(Vector2 pixelUV, Color color, int size)
    {
        /*for (int x = -size; x < 1; x++)
        {
            int newX = (int)pixelUV.x + x;
            if (newX > -1 && newX < 1501) {
                for (int y = -size - x; y < size + x + 1; y++)
                {
                    int newY = (int)pixelUV.y + y;
                    if (newY > -1 && newY < 2001)
                    {
                        Drawing.Texture.SetPixel((int)pixelUV.x + x, (int)pixelUV.y + y, color);
                    }
                }
            }
        }
        for(int x = 1; x < size + 1; x++)
        {
            int newX = (int)pixelUV.x + x;
            if (newX > -1 && newX < 1501)
            {
                for (int y = -size + x; y < size - x + 1; y++)
                {
                    int newY = (int)pixelUV.y + y;
                    if (newY > -1 && newY < 2001)
                    {
                        Drawing.Texture.SetPixel((int)pixelUV.x + x, (int)pixelUV.y + y, color);
                    }
                }
            }
        }*/
        for (int x = -size; x < size + 1; x++)
        {
            //int newX = (int)pixelUV.x + x;
            int maxY = 2 + (int)(Mathf.Sin((x + size) * Mathf.PI / (2 * size)) * size);
            int newX = (int)pixelUV.x + x;
            if (newX > -1 && newX < 1500)
            {
                for (int y = -maxY; y < maxY + 1; y++)
                {
                    int newY = (int)pixelUV.y + y;
                    if (newY > -1 && newY < 2000)
                    {
                        Drawing.Texture.SetPixel(newX, newY, color);
                    }
                }
            }
        }

        Drawing.Texture.Apply();
    }
}

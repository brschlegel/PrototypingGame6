using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Much of the below code taken from or inspired by https://unity3d.college/2017/07/22/build-unity-multiplayer-drawing-game-using-unet-unity3d/
/// </summary>

public enum Shape
{
    Circle,
    Square,
    Diamond
}

public class Paint : MonoBehaviour
{
    public static int brushSize;
    private static int brushSizeSquared;
    public static Shape brushShape;
    private void Start()
    {
        SetBrushSize(10);
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
                    BrushAreaWithColor(pixelUV, ColorPicker.SelectedColor, brushSize, brushSizeSquared);
                }
            }
        }
    }

    public static void SetBrushSize(int size)
    {
        brushSize = size;
        brushSizeSquared = size * size;
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

    private void BrushAreaWithColor(Vector2 pixelUV, Color color, int size, int sizeSqr)
    {
        switch (brushShape)
        {
            case Shape.Circle:
                for (int x = -size; x < size + 1; x++)
                {
                    int maxY = (int)Mathf.Sqrt(brushSizeSquared - (x * x));
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
                break;
            case Shape.Diamond:
                for (int x = -size; x < 1; x++)
                {
                    int newX = (int)pixelUV.x + x;
                    if (newX > -1 && newX < 1501) {
                        for (int y = -size - x; y < size + x + 1; y++)
                        {
                            int newY = (int)pixelUV.y + y;
                            if (newY > -1 && newY < 2001)
                            {
                                Drawing.Texture.SetPixel(newX, newY, color);
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
                                Drawing.Texture.SetPixel(newX, newY, color);
                            }
                        }
                    }
                }
                break;
            case Shape.Square:
                for (int x = -size; x < size + 1; x++)
                {
                    int newX = (int)pixelUV.x + x;
                    if (newX > -1 && newX < 1500)
                    {
                        for (int y = -size; y < size + 1; y++)
                        {
                            int newY = (int)pixelUV.y + y;
                            if (newY > -1 && newY < 2000)
                            {
                                Drawing.Texture.SetPixel(newX, newY, color);
                            }
                        }
                    }
                }
                break;
            default:
                break;
        }
        Drawing.Texture.Apply();
    }
}

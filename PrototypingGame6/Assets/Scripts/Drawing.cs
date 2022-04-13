using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Some of the below code taken from or inspired by https://unity3d.college/2017/07/22/build-unity-multiplayer-drawing-game-using-unet-unity3d/
/// </summary>

public class Drawing : MonoBehaviour
{
    public Texture2D texture;
    private void Start()
    {
        Texture = texture;
        Clear();
        GetComponent<Renderer>().material.mainTexture = Texture;
    }

    public static void Clear()
    {
        for (int c = 0; c < Texture.width; c++)
        {
            for (int d = 0; d < Texture.height; d++)
            {
                Texture.SetPixel(c, d, Color.white);
            }
        }
        Texture.Apply();
    }

    public static Texture2D Texture { get; private set; }

    public static byte[] GetAllTextureData()
    {
        return Texture.GetRawTextureData();
    }

    internal static void SetAllTextureData(byte[] textureData)
    {
        Texture.LoadRawTextureData(textureData);
        Texture.Apply();
    }
}

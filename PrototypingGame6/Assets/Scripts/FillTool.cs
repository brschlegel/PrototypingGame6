using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillTool : MonoBehaviour
{
    private bool[,] visitedBools;
    private Queue<(int, int)> queue;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                FillTool fillTool = hit.collider.GetComponent<FillTool>();
                if (fillTool != null)
                {
                    Paint.action = Actions.Filling;
                }
                else if (Paint.action == Actions.Filling)
                {
                    Drawing drawing = hit.collider.GetComponent<Drawing>();
                    if (drawing != null)
                    {
                        Renderer rend = hit.transform.GetComponent<Renderer>();

                        Texture2D tex = rend.material.mainTexture as Texture2D;
                        Vector2 pixelUV = hit.textureCoord;
                        pixelUV.x *= tex.width;
                        pixelUV.y *= tex.height;
                        visitedBools = new bool[1500, 2000];
                        int x = (int)pixelUV.x;
                        int y = (int)pixelUV.y;
                        queue = new Queue<(int, int)>();
                        queue.Enqueue((x, y));
                        visitedBools[x, y] = true;
                        Color color = ColorPicker.SelectedColor;
                        Color changeColor = Drawing.Texture.GetPixel(x, y);
                        while (queue.Count > 0)
                        {
                            (int, int) top = queue.Peek();
                            FillSquare(top.Item1, top.Item2, color, changeColor);
                            queue.Dequeue();
                        }
                        Drawing.Texture.Apply();
                    }
                }
            }
        }
    }

    private void FillSquare(int x, int y, Color color, Color changeColor)
    {
        Drawing.Texture.SetPixel(x, y, color);
        if (x > 0 && !visitedBools[x - 1, y] && Drawing.Texture.GetPixel(x - 1, y) == changeColor)
        {
            queue.Enqueue((x - 1, y));
            visitedBools[x - 1, y] = true;
        }
        if (x < 1499 && !visitedBools[x + 1, y] && Drawing.Texture.GetPixel(x + 1, y) == changeColor)
        {
            queue.Enqueue((x + 1, y));
            visitedBools[x + 1, y] = true;
        }
        if (y > 0 && !visitedBools[x, y - 1] && Drawing.Texture.GetPixel(x, y - 1) == changeColor)
        {
            queue.Enqueue((x, y - 1));
            visitedBools[x, y - 1] = true;
        }
        if (y < 1999 && !visitedBools[x, y + 1] && Drawing.Texture.GetPixel(x, y + 1) == changeColor)
        {
            queue.Enqueue((x, y + 1));
            visitedBools[x, y + 1] = true;
        }
        /*void FillSquare(int x, int y)
        {
            Drawing.Texture.SetPixel(x, y, color);
            visitedBools[x, y] = true;
            if (x > 0 && !visitedBools[x - 1, y] && Drawing.Texture.GetPixel(x - 1, y) == changeColor)
            {
                FillSquare(x - 1, y);
            }
            if (x < 1499 && !visitedBools[x + 1, y] && Drawing.Texture.GetPixel(x + 1, y) == changeColor)
            {
                FillSquare(x + 1, y);
            }
            if (y > 0 && !visitedBools[x, y - 1] && Drawing.Texture.GetPixel(x, y - 1) == changeColor)
            {
                FillSquare(x, y - 1);
            }
            if (y < 1999 && !visitedBools[x, y + 1] && Drawing.Texture.GetPixel(x, y + 1) == changeColor)
            {
                FillSquare(x, y + 1);
            }
        }
        FillSquare(x1, y1);*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undo : DrawingButtons
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Paint.actionStack.Count > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Undo undo = hit.collider.GetComponent<Undo>();
                if (undo != null)
                {
                    buttonGraying.GrayButtons(type, id);
                    List<(int, int, Color)> list = Paint.actionStack.Pop();
                    foreach((int, int, Color) c in list)
                    {
                        Drawing.Texture.SetPixel(c.Item1, c.Item2, c.Item3);
                    }
                    Drawing.Texture.Apply();
                }
            }
        }
    }
}

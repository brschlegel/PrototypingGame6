using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType
{
    BrushSize,
    Eraser,
    BrushShape,
    EyedropperFill,
    ClearSubmit
}

public class ButtonGraying : MonoBehaviour
{
    public Color clickShade;
    public Transform brushSizes;
    public Transform paintTools;
    public Renderer clearRend;
    public Renderer submitRend;
    public Renderer undoRend;

    public void GrayButtons(ButtonType type, int id)
    {
        switch (type)
        {
            case ButtonType.BrushSize:
                for(int c = 0; c < 5; c++)
                {
                    if(c == id)
                    {
                        brushSizes.GetChild(c).GetComponent<Renderer>().material.color = clickShade;
                    }
                    else
                    {
                        brushSizes.GetChild(c).GetComponent<Renderer>().material.color = Color.white;
                    }
                }
                break;
            case ButtonType.Eraser:
                paintTools.GetChild(0).GetComponent<Renderer>().material.color = clickShade;
                paintTools.GetChild(4).GetComponent<Renderer>().material.color = Color.white;
                paintTools.GetChild(5).GetComponent<Renderer>().material.color = Color.white;
                break;
            case ButtonType.BrushShape:
                paintTools.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
                for(int c = 1; c < 4; c++)
                {
                    if(c == id)
                    {
                        paintTools.GetChild(c).GetComponent<Renderer>().material.color = clickShade;
                    }
                    else
                    {
                        paintTools.GetChild(c).GetComponent<Renderer>().material.color = Color.white;
                    }
                }
                paintTools.GetChild(4).GetComponent<Renderer>().material.color = Color.white;
                paintTools.GetChild(5).GetComponent<Renderer>().material.color = Color.white;
                break;
            case ButtonType.EyedropperFill:
                for (int c = 0; c < 4; c++)
                {
                    paintTools.GetChild(c).GetComponent<Renderer>().material.color = Color.white;
                }
                for(int c = 4; c < 6; c++)
                {
                    if (c == id)
                    {
                        paintTools.GetChild(c).GetComponent<Renderer>().material.color = clickShade;
                    }
                    else
                    {
                        paintTools.GetChild(c).GetComponent<Renderer>().material.color = Color.white;
                    }
                }
                break;
            case ButtonType.ClearSubmit:
                if(id == 2)
                {
                    clearRend.material.color = clickShade;
                }
                else if(id == 3)
                {
                    submitRend.material.color = clickShade;
                }
                else
                {
                    undoRend.material.color = clickShade;
                }
                Invoke("ReWhite", .2f);
                break;
            default:
                break;
        }
    }

    private void ReWhite()
    {
        clearRend.material.color = Color.white;
        submitRend.material.color = Color.white;
        undoRend.material.color = Color.white;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingButtons : MonoBehaviour
{
    public int id;
    public ButtonType type;
    public ButtonGraying buttonGraying;

    private void Start()
    {
        buttonGraying = GameObject.Find("Main Camera/ButtonHolder").GetComponent<ButtonGraying>();
    }
}

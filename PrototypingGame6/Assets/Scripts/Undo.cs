using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UndoAction
{
    bool isFill; //If false, is paint
    List<(int, int, Color)> colors;
}

public class Undo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

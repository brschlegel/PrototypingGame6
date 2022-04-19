using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : DrawingButtons
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Clear clear = hit.collider.GetComponent<Clear>();
                if (clear != null)
                {
                    buttonGraying.GrayButtons(type, id);
                    Drawing.Clear();
                }
            }
        }
    }
}

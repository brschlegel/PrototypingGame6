using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                FinishScript submit = hit.collider.GetComponent<FinishScript>();
                if (submit != null)
                {
                    //Your submit shizzle here
                }
            }
        }
    }
}

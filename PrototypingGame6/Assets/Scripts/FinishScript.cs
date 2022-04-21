using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                    SceneManager.LoadScene("SubmitScene");
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScript : MonoBehaviour
{
    public AlienShuffler alienShuffler;
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

                    for(int i = 0; i < alienShuffler.aliens.Count; i++)
                    {
                        ResultData r;
                        r.sprite = alienShuffler.aliens[i].GetComponent<Image>().sprite;
                        r.score = alienShuffler.aliens[i].score;
                        Results.results.Add(r);
                    }
                    SceneManager.LoadScene("SubmitScene");
                }
            }
        }
    }
}

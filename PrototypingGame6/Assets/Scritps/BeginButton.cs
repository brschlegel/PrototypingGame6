using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BeginButton : MonoBehaviour
{

    public float delay;
    public Transform button;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Enable());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Enable()
    {
        button.gameObject.SetActive(false);
        yield return new WaitForSeconds(delay);
        button.gameObject.SetActive(true);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("JulienScene");
    }

}

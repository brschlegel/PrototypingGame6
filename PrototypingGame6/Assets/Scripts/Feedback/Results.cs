using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Results : MonoBehaviour
{
    [SerializeField]
    List<GameObject> aliens;
    [SerializeField]
    GameObject finalScore;
    [SerializeField]
    GameObject goBackButton;
    void Start()
    {
        StartCoroutine(ShowResults());
        int sum = 0;
        foreach(GameObject go in aliens)
        {
            int score = Random.Range(0, 101);
            go.GetComponentInChildren<TextMeshProUGUI>().text = score.ToString();
            sum += score;
        }
        finalScore.GetComponentInChildren<TextMeshProUGUI>().text = (sum/8).ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowResults()
    {
        for(int i = 0; i < 8; i++)
        {
            aliens[i].SetActive(true);
            yield return new WaitForSeconds(.3f);
        }
        finalScore.SetActive(true);
        goBackButton.SetActive(true);
    }
}

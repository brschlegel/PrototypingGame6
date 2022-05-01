using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public struct ResultData
{
    public Sprite sprite;
    public int score;
}
public class Results : MonoBehaviour
{
    public static List<ResultData> results;   
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
        for(int i = 0; i < aliens.Count; i++)   
        {
            int score = results[i].score;
            aliens[i].GetComponentInChildren<TextMeshProUGUI>().text = score.ToString();
            aliens[i].transform.Find("Alien").GetComponent<Image>().sprite = results[i].sprite;
            sum += score;
        }
        finalScore.GetComponentInChildren<TextMeshProUGUI>().text = (sum/aliens.Count).ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowResults()
    {
        for(int i = 0; i < aliens.Count; i++)
        {
            aliens[i].SetActive(true);
            yield return new WaitForSeconds(.3f);
        }
        finalScore.SetActive(true);
        goBackButton.SetActive(true);
    }

    public void WriteResults()
    {
        using (StreamWriter sw = File.AppendText(Application.dataPath + "/" + "results.csv"))
        {
            for (int i = 0; i < results.Count; i++)
            {

                if (i != results.Count - 1)
                {
                    sw.Write(results[i].score);
                    sw.Write(',');

                }
                else
                {
                    sw.WriteLine(results[i].score);
                }
            }
        }
    }
}

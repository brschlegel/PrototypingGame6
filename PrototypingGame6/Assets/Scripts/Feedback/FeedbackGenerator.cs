using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackGenerator : MonoBehaviour
{
    private Dictionary<string, List<string>> subjectData;

    public static int negativeScoreCutoff = 30;
    public static int neutralScoreCutoff = 70;
    public static int positiveScoreCutoff = 100;

    [SerializeField]
    private int scoreVariance;
    [SerializeField]
    private float afterThoughtChance;

    [SerializeField]
    private Descriptors colorDescriptors;
    [SerializeField]
    private Descriptors brushSizeDescriptors;
    [SerializeField]
    private Descriptors brushTypeDescriptors;
    private void Start()
    {
        Debug.Log(string.Format("ds is formatted much better than {1}", "Fisr", "SMa"));
    }

    public void PopulateSubjectData()
    {
        subjectData = new Dictionary<string,List<string>>(PaintingData.data);
    }
    
    public string GenerateFeedback(string color, string size, string type)
    {
        int rand = Random.Range(0, 3);
        string possible;
        switch(rand)
        {
            case 0:
               
                possible = GetPossible(PaintingData.possibleData["Color"], color);
                return GetFeedbackString(colorDescriptors, color, possible);
                
            case 1:
               
                possible = GetPossible(PaintingData.possibleData["BrushSize"], size);
                return GetFeedbackString(brushSizeDescriptors, size, possible);
            case 2:
               
                possible = GetPossible(PaintingData.possibleData["BrushType"], type);
                return GetFeedbackString(brushTypeDescriptors, type, possible);


        }
        return "How did we get here?";
    }

    private string GetFeedbackString(Descriptors d, string subject, string possible)
    {
        int relativeScore = PaintingData.score + Random.Range(-scoreVariance, scoreVariance);   
        string s = d.GetPredicate(relativeScore);
        if(Random.value < afterThoughtChance)
        {
            s += d.GetAfterThought(relativeScore);
            return string.Format(s, subject, possible);
        }

        return string.Format(s, subject);
    }

    private string GetPossible(List<string> l, string subject)
    {
        List<string> possible = new List<string>(l);
        possible.Remove(subject);
        return possible[Random.Range(0, possible.Count)];
    }

    private void Update()
    {

    }
}

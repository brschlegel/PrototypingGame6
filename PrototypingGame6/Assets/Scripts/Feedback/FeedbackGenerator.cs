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
        PaintingData.SetScore();
        PaintingData.RandomTests(4);
        PopulateSubjectData();
        Debug.Log(GenerateFeedback());
        Debug.Log(GenerateFeedback());
    }

    public void PopulateSubjectData()
    {
        subjectData = new Dictionary<string,List<string>>(PaintingData.data);
    }
    
    public string GenerateFeedback()
    {
        int rand = Random.Range(0, 3);
        string subject;
        string possible;
        switch(rand)
        {
            case 0:
                subject = subjectData["Color"][Random.Range(0, subjectData["Color"].Count)];
                possible = PaintingData.possibleData["Color"][Random.Range(0, subjectData["Color"].Count)];
                return GetFeedbackString(colorDescriptors, subject, possible);
                
            case 1:
                subject = subjectData["BrushSize"][Random.Range(0, subjectData["BrushSize"].Count)];
                possible = PaintingData.possibleData["BrushSize"][Random.Range(0, subjectData["BrushSize"].Count)];
                return GetFeedbackString(brushSizeDescriptors, subject, possible);
            case 2:
                subject = subjectData["BrushType"][Random.Range(0, subjectData["BrushType"].Count)];
                possible = PaintingData.possibleData["BrushType"][Random.Range(0, subjectData["BrushType"].Count)];
                return GetFeedbackString(colorDescriptors, subject, possible);


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
}

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

    [SerializeField]
    AlienShuffler alienShuffler;
    private void Start()
    {
       
    }

    public void PopulateSubjectData()
    {
        subjectData = new Dictionary<string,List<string>>(PaintingData.data);
    }
    
    public string GenerateFeedback(string color, string size, string type)
    {
        int rand = Random.Range(0, 3);
       
        switch(rand)
        {
            case 0:
                return GetFeedbackString(colorDescriptors, color);
            case 1:
                return GetFeedbackString(brushSizeDescriptors, size);
            case 2:
                return GetFeedbackString(brushTypeDescriptors, type);
        }
        return "How did we get here?";
    }

    private string GetFeedbackString(Descriptors d, string subject)
    {
        Alien a = alienShuffler.aliens[Random.Range(0, 3)];
        int relativeScore = a.GetOpinion(subject);
        alienShuffler.SetPointer(a);
        string s = d.GetPredicate(relativeScore);

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

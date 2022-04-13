using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Descriptors")]
public class Descriptors : ScriptableObject
{
    [Header("Predicates")]
    public List<string> positivePredicates;
    public List<string> neutralPredicates;
    public List<string> negativePredicates;
    [Header("Afterthoughts")]
    public List<string> positiveAfterThought;
    public List<string> neutralAfterThought;
    public List<string> negativeAfterThought;

    public string GetPredicate(int relativeScore)
    {
        if( relativeScore <= FeedbackGenerator.negativeScoreCutoff)
        {
            return negativePredicates[Random.Range(0, negativePredicates.Count)];   
        }
        else if (relativeScore <= FeedbackGenerator.neutralScoreCutoff)
        {
            return neutralPredicates[Random.Range(0, neutralPredicates.Count)];
        }
        else
        {
            return positivePredicates[Random.Range(0, positivePredicates.Count)];
        }
    }

    public string GetAfterThought(int relativeScore)
    {
        if (relativeScore <= FeedbackGenerator.negativeScoreCutoff)
        {
            return negativeAfterThought[Random.Range(0, negativeAfterThought.Count)];
        }
        else if (relativeScore <= FeedbackGenerator.neutralScoreCutoff)
        {
            return neutralAfterThought[Random.Range(0, neutralAfterThought.Count)];
        }
        else
        {
            return positiveAfterThought[Random.Range(0, positiveAfterThought.Count)];
        }
    }
}

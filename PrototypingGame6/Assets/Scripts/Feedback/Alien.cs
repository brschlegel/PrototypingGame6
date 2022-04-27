using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AlienOpinion { Positive, Neutral, Negative}
public class Alien : MonoBehaviour
{
  
    public List<string> likes;
    public List<string> dislikes;
    public int score;
    public int likeIncrease = 5;
    public int dislikeDecrease = 5;
    private void Start()
    {
     
        score = 50;
    }

    //is it ugly? yes. Inefficient? yes. But does it get the job done? Maybe
    public void UpdateScore(string color, string size, string type)
    {
        
        foreach(string l in likes)
        {
            if(l == color || l == size || l == type)
            {
                score += likeIncrease;
               
            }
          
        }

        //Yes this means the alien disliking a part of the stroke will overwrite their overall opinion, but im kinda fine with that. Call it an artist statement B)
        foreach(string d in dislikes)
        {
            if(d == color || d == size || d == type)
            {
                score -= dislikeDecrease;
               
            }
        }

        score = Mathf.Clamp(score, 0, 100);
    }

    public int GetOpinion(string facet)
    {
        foreach(string d in dislikes)
        {
            if(d == facet)
            {
                return 10;
            }
        }
        foreach(string l in likes)
        {
            if(l == facet)
            {
                return 90;
            }
        }
        return 50;
    }
}

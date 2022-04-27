using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienPreferences : ScriptableObject
{
    //Not entirely sure why i didn't make these enums tbh
    public List<string> likes;
    public List<string> dislikes;

    public Sprite sprite;
}

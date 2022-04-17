using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class PaintingData
{
    //Theres probably a more elegant solution here with a dictionary of strings or something but eh we chillin
    //Update: we were not chillin, ended up changing it lol


    public static Dictionary<string, List<string>> data = 
        new Dictionary<string, List<string>>() { {"Color", new List<string>()}, {"BrushSize", new List<string>() }, {"BrushType", new List<string>() } };

    public static Dictionary<string, List<string>> possibleData = new Dictionary<string, List<string>>()
    {{"Color", new List<string>() { "Red", "Blue", "Green" }},{"BrushSize", new List<string>() { "Extra Small", "Small", "Medium", "Large", "Extra Large" } },{"BrushType", new List<string>() { "Square", "Circle","Diamond" }} };
   

    public static int score;


    public static void SetScore()
    {
        score = Random.Range(0, 102);
    }

    public static void LogColor(string c)
    {
        if(!data["Color"].Contains(c))
            data["Color"].Add(c);
    }

    public static void LogBrushSize(string s)
    {
        if (!data["BrushSize"].Contains(s))
            data["BrushSize"].Add(s);
    }

    public static void LogBrushType(string t)
    {
        if (!data["BrushType"].Contains(t))
            data["BrushType"].Add(t);
    }
    //TODO: Add painting number to file, so each painting doesn't overwrite another
    public static void WriteData(string path)
    {
        path += ".csv";
        using(StreamWriter w = new StreamWriter(path))
        {
            foreach(string t in possibleData.Keys)
            {
                w.Write(t);
                for (int i = 0; i < data[t].Count; i++)
                {
                    w.Write(",");
                    w.Write(data[t][i]);
                }
                w.WriteLine();
            }

         
        }
    }
    //Used to test the system
    public static void RandomTests(int numTests)
    {
        for (int i = 0; i < numTests; i++)
        {
            LogColor(possibleData["Color"][Random.Range(0, possibleData["Color"].Count)]);
            LogBrushSize(possibleData["BrushSize"][Random.Range(0, possibleData["BrushSize"].Count)]);
            LogBrushType(possibleData["BrushType"][Random.Range(0, possibleData["BrushType"].Count)]);
        }
    }
}

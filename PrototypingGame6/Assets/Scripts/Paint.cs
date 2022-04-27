using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Some of the below code inspired by https://unity3d.college/2017/07/22/build-unity-multiplayer-drawing-game-using-unet-unity3d/
/// </summary>

public enum Shape
{
    Circle,
    Square,
    Diamond
}

public enum Actions
{
    Painting,
    Eyedropping,
    Filling
}

public class Paint : MonoBehaviour
{
    public static int brushSize;
    private static int brushSizeSquared;
    public static Shape brushShape;
    public static Dictionary<string, int> colorQuantities;
    public static bool isEraser;
    public static Actions action;
    public static Stack<List<(int, int, Color)>> actionStack;
    int pixelsToAdd;
    private bool onBoard;
    private List<(int, int, Color)> colorList;

    [SerializeField]
    private FeedbackGenerator feedbackGenerator;
    [SerializeField]
    private TextMeshProUGUI feedbackText;
    [SerializeField]
    private Transform holdHints;
    [SerializeField]
    private AlienShuffler alienShuffler;

    private bool currentlyDrawing;
    private void Start()
    {
        actionStack = new Stack<List<(int, int, Color)>>();
        colorList = new List<(int, int, Color)>();
        action = Actions.Painting;
        SetBrushSize(10);
        colorQuantities = new Dictionary<string, int>()
        {
            {"LightColor", 0}, //All values above .85
            {"Black", 0}, //All values are below .15
            {"Blue", 0},  //Blue dominant and nothing else comes close
            {"Red", 0},   //Red dominant and nothing else comes close
            {"Green", 0}, //Green dominant and nothing else comes close, if more than 80 percent blue then is blue
            {"Yellow", 0}, //Red dominant AND more than 80 percent green AND less than 50 percent blue OR the same but with red and green flipped
            {"Purple", 0}, //Blue dominant AND more than 50 percent red AND less than 83 percent red AND less than 80 percent green
            {"Orange", 0}, //Red dominant AND more than 40 percent green AND less than 80 percent green AND less than 50 percent blue
            {"Pink", 0}     //Red dominant AND more than 40 percent blue OR Blue dominant but more than 83 percent red (green must be the least dominant)
        };
        ColorPicker.SelectedColor = Color.white;
        ColorPicker.BaseColor = Color.white;
        StartCoroutine(RequestFeedback());  
    }

    public void ChangeHintColor()
    {
        Color color = ColorPicker.SelectedColor;
        color.a = .7f;
        for (int c = 0; c < 3; c++)
        {
            holdHints.GetChild(c).GetComponent<Renderer>().material.color = color;
        }
    }

    public void ChangeHintShape(Shape shape)
    {
        int shapeInt = (int)shape;
        for (int c = 0; c < 3; c++)
        {
            if (c == shapeInt)
            {
                holdHints.GetChild(c).gameObject.SetActive(true);
            }
            else
            {
                holdHints.GetChild(c).gameObject.SetActive(false);
            }
        }
    }

    public void ChangeHintSize(int size)
    {
        float newSize = size / 125f;
        holdHints.localScale = new Vector3(newSize, newSize, 1f);
    }

    private void Update()
    {
        currentlyDrawing = false;
        RaycastHit hit;
        if (action == Actions.Painting && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Drawing drawing = hit.collider.GetComponent<Drawing>();
            if (drawing != null)
            {
                if (!onBoard)
                {
                    onBoard = true;
                    holdHints.gameObject.SetActive(true);
                }
                holdHints.transform.position = hit.point - (hit.transform.forward / 10000f);
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    currentlyDrawing = true;
                    Renderer rend = hit.transform.GetComponent<Renderer>();

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;

                    pixelsToAdd += BrushAreaWithColor(pixelUV, ColorPicker.SelectedColor, brushSize, brushSizeSquared);
                }
            }
            else if (onBoard)
            {
                onBoard = false;
                holdHints.gameObject.SetActive(false);
            }

        }
        else if (onBoard)
        {
            onBoard = false;
            holdHints.gameObject.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && colorList.Count > 0)
        {
            actionStack.Push(new List<(int, int, Color)>(colorList));
            colorList.Clear();
        }
        /*if (action == Actions.Painting && Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Drawing drawing = hit.collider.GetComponent<Drawing>();
                if (drawing != null)
                {
                    currentlyDrawing = true;
                    Renderer rend = hit.transform.GetComponent<Renderer>();

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;

                    pixelsToAdd += BrushAreaWithColor(pixelUV, ColorPicker.SelectedColor, brushSize, brushSizeSquared);
                }
            }
        }*/
        /*else if (Input.GetKeyUp(KeyCode.Mouse0) && pixelsToAdd > 0)
        {
            AddToQuantities();
        }*/
    }

    private string ColorToString()
    {
        Color c = ColorPicker.SelectedColor;
        float domColor = Mathf.Max(new float[] { c.r, c.g, c.b });
        if (domColor < .15f)
        {
            return "Black";
        }
        else if(c.r > .85f && c.g > .85f && c.b > .85f)
        {
            return "White";
        }
        else if(Mathf.Abs(c.r - c.g) < .06f && Mathf.Abs(c.r - c.b) < .06f && Mathf.Abs(c.g - c.b) < .06f)
        {
            return "Gray";
        }
        else
        {
            if (c.r == domColor)
            {
                if (c.b > c.g && c.b * 2.5f > c.r)
                {
                    return "Pink";
                }
                else if (c.g * 2.5f > c.r && c.g * 1.25f < c.r && c.b * 2f < c.r)
                {
                    return "Orange";
                }
                else if (c.g * 1.25f > c.r && c.b * 2f < c.r)
                {
                    return "Yellow";
                }
                else
                {
                    return "Red";
                }
            }
            else if (c.g == domColor)
            {
                if (c.r * 1.25f > c.g && c.b * 2f < c.g)
                {
                    return "Yellow";
                }
                else if(c.b * 1.25f > c.g)
                {
                    return "Blue";
                }
                else
                {
                    return "Green";
                }
            }
            else
            {
                if(c.r * 2f > c.b && c.r * 1.2f < c.b && c.g * 1.25f < c.b)
                {
                    return "Purple";
                }
                else if(c.r > c.g && c.r * 1.2f > c.b)
                {
                    return "Pink";
                }
                else
                {
                    return "Blue";
                }
            }
        }
        //pixelsToAdd = 0;

    }

    public static void SetBrushSize(int size)
    {
        brushSize = size;
        brushSizeSquared = size * size;
    }


    private int BrushAreaWithColor(Vector2 pixelUV, Color color, int size, int sizeSqr)
    {
        int totalPixels = 0;
        switch (brushShape)
        {
            case Shape.Circle:
                for (int x = -size; x < size + 1; x++)
                {
                    int maxY = (int)Mathf.Sqrt(brushSizeSquared - (x * x));
                    int newX = (int)pixelUV.x + x;
                    if (newX > -1 && newX < 1500)
                    {
                        for (int y = -maxY; y < maxY + 1; y++)
                        {
                            int newY = (int)pixelUV.y + y;
                            if (newY > -1 && newY < 2000)
                            {
                                Color colorChange = Drawing.Texture.GetPixel(newX, newY);
                                if (colorChange != color)
                                {
                                    colorList.Add((newX, newY, colorChange));
                                    Drawing.Texture.SetPixel(newX, newY, color);
                                    totalPixels++;
                                }
                            }
                        }
                    }
                }
                break;
            case Shape.Diamond:
                for (int x = -size; x < 1; x++)
                {
                    int newX = (int)pixelUV.x + x;
                    if (newX > -1 && newX < 1501) {
                        for (int y = -size - x; y < size + x + 1; y++)
                        {
                            int newY = (int)pixelUV.y + y;
                            if (newY > -1 && newY < 2001)
                            {
                                Color colorChange = Drawing.Texture.GetPixel(newX, newY);
                                if (colorChange != color)
                                {
                                    colorList.Add((newX, newY, colorChange));
                                    Drawing.Texture.SetPixel(newX, newY, color);
                                    totalPixels++;
                                }
                            }
                        }
                    }
                }
                for(int x = 1; x < size + 1; x++)
                {
                    int newX = (int)pixelUV.x + x;
                    if (newX > -1 && newX < 1501)
                    {
                        for (int y = -size + x; y < size - x + 1; y++)
                        {
                            int newY = (int)pixelUV.y + y;
                            if (newY > -1 && newY < 2001)
                            {
                                Color colorChange = Drawing.Texture.GetPixel(newX, newY);
                                if (colorChange != color)
                                {
                                    colorList.Add((newX, newY, colorChange));
                                    Drawing.Texture.SetPixel(newX, newY, color);
                                    totalPixels++;
                                }
                            }
                        }
                    }
                }
                break;
            case Shape.Square:
                for (int x = -size; x < size + 1; x++)
                {
                    int newX = (int)pixelUV.x + x;
                    if (newX > -1 && newX < 1500)
                    {
                        for (int y = -size; y < size + 1; y++)
                        {
                            int newY = (int)pixelUV.y + y;
                            if (newY > -1 && newY < 2000)
                            {
                                Color colorChange = Drawing.Texture.GetPixel(newX, newY);
                                if (colorChange != color)
                                {
                                    colorList.Add((newX, newY, colorChange));
                                    Drawing.Texture.SetPixel(newX, newY, color);
                                    totalPixels++;
                                }
                            }
                        }
                    }
                }
                break;
            default:
                break;
        }
        Drawing.Texture.Apply();
        return totalPixels;
    }

    private IEnumerator RequestFeedback()
    {
        while(gameObject.activeSelf)
        {
            if(currentlyDrawing)
            {
                PaintingData.SetScore();
                 feedbackText.text = feedbackGenerator.GenerateFeedback(ColorToString(), BrushSizeToString(brushSize), brushShape.ToString());
                foreach(Alien a in alienShuffler.aliens)
                {
                    a.UpdateScore(ColorToString(), BrushSizeToString(brushSize), brushShape.ToString());
                }
            }
            yield return new WaitForSeconds(Random.Range(2,6));

        }
    }

    private string BrushSizeToString(int i)
    {
        switch(i)
        {
            case 10:
                return "Extra Small";
            case 20:
                return "Small";
            case 30:
                return "Medium";
            case 40:
                return "Large";
            case 50:
                return "Extra Large";
        }
        return "How could this happen to me";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienShuffler : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> alienSprites;

    [SerializeField]
    private RectTransform pointer;

    void Start()
    {
        foreach(Transform c in transform)
        {
            int randIndex = (int)(Random.value * alienSprites.Count);
            c.GetComponent<Image>().sprite = alienSprites[randIndex];
            alienSprites.RemoveAt(randIndex);   

        }
        StartCoroutine(ReplaceAlien());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ReplaceAlien()
    {
        while (gameObject.activeSelf)
        {

            yield return new WaitForSeconds(Random.Range(15, 40));

            int randSpriteIndex = (int)(Random.value * alienSprites.Count);
            int randChildIndex = (int)(Random.value * transform.childCount);
            Image i = transform.GetChild(randChildIndex).GetComponent<Image>();
            Sprite old = i.sprite;
            Sprite randSprite = alienSprites[randSpriteIndex];
            i.sprite = randSprite;
            alienSprites.RemoveAt(randSpriteIndex);
            alienSprites.Add(old);
        }
    }

    public void SetPointerRandom()
    {
        int rand = (int)(Random.value * transform.childCount);
        Debug.Log("rand: " + rand);
        pointer.anchoredPosition = new Vector3(50 + rand*100, pointer.anchoredPosition.y);
    }
}

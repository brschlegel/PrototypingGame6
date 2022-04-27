using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlienShuffler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> alienPool;

    public List<Alien> aliens;
    [SerializeField]
    private RectTransform pointer;

    
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, alienPool.Count);
            GameObject g = alienPool[rand];
            alienPool.RemoveAt(rand);
            aliens.Add(Instantiate(g, transform).GetComponent<Alien>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public IEnumerator ReplaceAlien()
    //{
    //    while (gameObject.activeSelf)
    //    {

    //        yield return new WaitForSeconds(Random.Range(15, 40));

    //        int randSpriteIndex = (int)(Random.value * alienSprites.Count);
    //        int randChildIndex = (int)(Random.value * transform.childCount);
    //        Image i = transform.GetChild(randChildIndex).GetComponent<Image>();
    //        Sprite old = i.sprite;
    //        Sprite randSprite = alienSprites[randSpriteIndex];
    //        i.sprite = randSprite;
    //        alienSprites.RemoveAt(randSpriteIndex);
    //        alienSprites.Add(old);
    //    }
    //}

    public void SetPointer(Alien a)
    {
        int p = aliens.IndexOf(a);
      
        pointer.anchoredPosition = new Vector3(50 + p*100, pointer.anchoredPosition.y);
    }
}

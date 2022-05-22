using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsManager : MonoBehaviour
{

    [HideInInspector]
    public List<GameObject> listOfHearts = new List<GameObject>();

    public GameObject heart;

    public Sprite fullHeart;
    public Sprite threeQuartersHeart;
    public Sprite halfHeart;
    public Sprite quarterHeart;
    public Sprite emptyHeart;

    void Start()
    {
        for (int i = 0; i < (int)Player.maxHealth; i++)
        {
            float horizontalMove = (i % 12) * 30 ;
            float verticalMove = Mathf.Floor(i / 12) * 30;
            GameObject currentHeart = Instantiate(heart, GameObject.Find("HeartsContainer").transform);
            currentHeart.transform.localPosition = new Vector3(currentHeart.transform.localPosition.x + horizontalMove, currentHeart.transform.localPosition.y + verticalMove, 0f);
            listOfHearts.Add(currentHeart);
        }

        // positionToDisplay = heart.GetComponent<RectTransform>();
    }
    void Update()
    {
        for (int i = 0; i < listOfHearts.Count; i++)
        {
            if ((float)(i + 1) - Player.currHealth < 1 && (float)(i + 1) - Player.currHealth > 0)
            {
                float findDecimal = 1 - ((float)(i+1) - Player.currHealth);
                findDecimal = Mathf.Ceil(findDecimal / 0.25f);
                switch (findDecimal)
                {
                    case 1:
                        listOfHearts[i].GetComponent<Image>().sprite = quarterHeart;
                        break;

                    case 2:
                        listOfHearts[i].GetComponent<Image>().sprite = halfHeart;
                        break;

                    case 3:
                        listOfHearts[i].GetComponent<Image>().sprite = threeQuartersHeart;
                        break;
                    case 4:
                        listOfHearts[i].GetComponent<Image>().sprite = fullHeart;
                        break;
                }
            } else if (i + 1 <= Player.currHealth)
            {
                listOfHearts[i].GetComponent<Image>().sprite = fullHeart;
            } else
            {
                listOfHearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }
}

using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpinScript : MonoBehaviour
{
    public List<GameObject> iconList;
    [SerializeField] int distanceBetweenIcons;
    RectTransform rectTransform;
    public GameObject targetIcon; //The one that is selected when players hit space

    public float wheelOffset;
    public float spinSpeed;

    public bool isSpinning;

    private float startPos;

    [SerializeField] GameObject iconPlaceholder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if(gameObject.transform.childCount > 0) UpdateIconList();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!CombatScript.main.inCombat)
        {
            SpinLogic();
        }
    }

    private void SpinLogic()
    {
        if (isSpinning)
        {
            float totalLength = 0;
            wheelOffset += spinSpeed * Time.deltaTime;
            totalLength = iconList.Count * distanceBetweenIcons;

            for (int i = 0; i < iconList.Count; i++)
            {
                float x = i * distanceBetweenIcons - wheelOffset;

                // wrap into range [0, totalLength)
                x = Mathf.Repeat(x - startPos, totalLength) + startPos;

                iconList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
            }
        }
        else
        {
            if (targetIcon != null)
            {
                if (targetIcon.GetComponent<RectTransform>().anchoredPosition.x <= 0)
                {
                    Debug.Log("Stop spin!");
                }
                else
                {
                    float totalLength = 0;
                    wheelOffset += spinSpeed * Time.deltaTime;
                    totalLength = iconList.Count * distanceBetweenIcons;

                    for (int i = 0; i < iconList.Count; i++)
                    {
                        float x = i * distanceBetweenIcons - wheelOffset;

                        // wrap into range [0, totalLength)
                        x = Mathf.Repeat(x - startPos, totalLength) + startPos;

                        iconList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
                    }
                }

            }
        }

        if (Input.GetKeyDown(KeyCode.R)) UpdateIconList();

        if (Input.GetKeyDown(KeyCode.T)) RemoveIcon(0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isSpinning) StopSpin();
            else StartSpin();
        }
    }
    
    public void RemoveIcon(int index)
    {
        //Kolla så att index ligger innanför listans längd. Om den gör det,
        //ta bort den ikonen.
        if (index < iconList.Count)
        {
            Destroy(iconList[index]);
            iconList.RemoveAt(index);
        }
        UpdateIconList();
    }

    public void AddIcon(GameObject itemToAdd)
    {
        GameObject instance = Instantiate(iconPlaceholder, transform, false);
        instance.GetComponent<Image>().sprite = itemToAdd.GetComponent<SpriteRenderer>().sprite;
        instance.GetComponent<ItemSymbol>().targetPosition = itemToAdd.transform;
        iconList.Add(instance);
        UpdateIconList();
    }

    public void UpdateIconList()
    {
        //Empty the list before refilling it
        iconList.Clear();

        int itemCount = gameObject.transform.childCount - 1;

        //The distance to add between each element in the list
        Vector2 addedDistance = Vector2.zero;

        //Create the conditions for the first item to spawn. It should create a centered alignement.
        float centerAlignment = itemCount * distanceBetweenIcons / 2f;
        startPos = rectTransform.anchoredPosition.x - centerAlignment;
        //spawnLocation.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - centerAlignment, 0);



        foreach (Transform child in gameObject.transform)
        {
            RectTransform rect = child.transform.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(startPos, 0) + addedDistance;
            //child.transform.position = transform.position + addedDistance; 
            addedDistance = new Vector2(addedDistance.x + distanceBetweenIcons, addedDistance.y);
            iconList.Add(child.gameObject);
        }
    }

    public void StartSpin()
    {
        wheelOffset = 0; //Behövs nog inte men känns bättre eftersom detta värde annars blir väldigt stort.
        targetIcon = null;
        isSpinning = true;
    }

    public void StopSpin()
    {
        float nearestX = float.MaxValue;


        foreach(GameObject item in iconList)
        {
            float value = item.GetComponent<RectTransform>().anchoredPosition.x;

            if(value > 0 && value < nearestX)
            {
                nearestX = value;
                targetIcon = item;
            }
        }
        ActivateChosenItem();
        isSpinning = false;
    }

    public void ActivateChosenItem()
    {
        //KOD som körs när man stannat!
        if (targetIcon != null) targetIcon.GetComponent<ItemSymbol>().ExecuteAction();
    }
}

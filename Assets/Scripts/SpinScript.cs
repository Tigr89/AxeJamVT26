using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public List<GameObject> iconList;
    [SerializeField] int distanceBetweenIcons;
    RectTransform rectTransform;

    public float wheelOffset;
    public float spinSpeed;

    public bool isSpinning;

    public RectTransform startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform;
        UpdateIconList();

        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Startpos: " + startPos.position);
        Debug.Log("Start anchorpos: " + startPos.anchoredPosition);
        if (isSpinning)
        {
            float totalLength = 0;
            wheelOffset += spinSpeed * Time.deltaTime;
            totalLength = iconList.Count * distanceBetweenIcons;

            for (int i = 0; i < iconList.Count; i++)
            {
                float x = i * distanceBetweenIcons - wheelOffset;

                // wrap into range [0, totalLength)
                x = Mathf.Repeat(x, totalLength);

                iconList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) UpdateIconList();

        if (Input.GetKeyDown(KeyCode.T)) RemoveIcon(2);
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
        
    }

    public void AddIcon(GameObject itemToAdd)
    {
        GameObject instance = Instantiate(itemToAdd, transform, false);
        iconList.Add(instance);
        UpdateIconList();
    }

    public void UpdateIconList()
    {
        rectTransform.anchoredPosition = startPos.anchoredPosition;
        

        Vector2 addedDistance = Vector2.zero;

        foreach (Transform child in gameObject.transform)
        {
            RectTransform rect = child.transform.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0) + addedDistance;
            //child.transform.position = transform.position + addedDistance; 
            addedDistance = new Vector2(addedDistance.x + distanceBetweenIcons, addedDistance.y);
            iconList.Add(child.gameObject);
        }
    }

    public void StartSpin()
    {
        
    }
}

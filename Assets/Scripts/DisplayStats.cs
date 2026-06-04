using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour
{
    public int health;
    public int damage;
    public int armour;
    [SerializeField] Sprite[] shieldSprites;
    [SerializeField] private Image shieldImage;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (armour > 0)
        {
            if (armour < shieldSprites.Length) shieldImage.sprite = shieldSprites[armour];
        }
        else shieldImage.sprite = shieldSprites[0];

    }
}

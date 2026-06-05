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

    [SerializeField] Sprite[] swordRank;
    [SerializeField] private Image swordImage;

    [SerializeField] private Image HPBar;
    public EnemyStats enemyInstance;

    public bool isPlayer; //Quickfix


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        float maxHP;
        float currentHP;

        if (enemyInstance == null) //Om enemyInstance är tom så betyder det att det är spelarens hälsa. Eller att vi kopplat fel.
        {
            maxHP = PlayerStats.main.playerMaxHealth;
            currentHP = PlayerStats.main.playerHealth;
            armour = PlayerStats.main.playerArmour;
            damage = PlayerStats.main.playerDamage;
        }
        else
        {
            currentHP = enemyInstance.enemyHealth;
            armour = enemyInstance.enemyArmour;
            maxHP = enemyInstance.enemyMaxHealth;
            
        }

        //UPDATE ARMOUR
        if (armour > 0)
        {
            if (armour < shieldSprites.Length) shieldImage.sprite = shieldSprites[armour];
        }
        else
        {
            shieldImage.sprite = shieldSprites[0];
            shieldImage.color = Color.grey;
        }

        if(damage > 0)
        {
            if (damage < swordRank.Length) swordImage.sprite = swordRank[damage];
        }
        else
        {
            if (swordRank.Length > 0) swordImage.sprite = swordRank[0];
            swordImage.color = Color.grey;
        }

        if(currentHP > 0 && enemyInstance != null && !isPlayer) HPBar.fillAmount = currentHP / maxHP;
        else if(isPlayer) HPBar.fillAmount = currentHP / maxHP;
        else HPBar.fillAmount = 0;

    }
}

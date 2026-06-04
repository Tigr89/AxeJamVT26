using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats main;
    public int playerMaxHealth = 36;
    public int playerHealth;
    public int playerDamage;
    public int playerArmour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (main == null) main = this;
        else Destroy(main);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlayerHealth(int amount)
    {
        playerHealth += amount;

        if(playerHealth <= 0)
        {
            Debug.Log("Game Over!");
        }
        if (playerHealth > playerMaxHealth) playerHealth = playerMaxHealth;
    }
}

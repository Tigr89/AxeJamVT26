using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats main;
    public int playerMaxHealth = 36;
    public int playerHealth;
    public int playerDamage;
    public int playerArmour;
    public int playerLevel = 1;

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
            SceneManager.LoadScene("GameOver");
        }
        if (playerHealth > playerMaxHealth) playerHealth = playerMaxHealth;
    }

    public void LevelUp()
    {
        playerLevel++;
        playerDamage++;
        Debug.Log("Level up! Nu är spelaren level " + playerLevel);
    }
}

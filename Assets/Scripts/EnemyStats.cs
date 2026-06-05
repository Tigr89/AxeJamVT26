using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStats : MonoBehaviour
{
    [Header("Base stats (set in Inspector)")]
    public int baseHealth = 11;
    public int baseMaxDMG = 3;
    public int baseMinDMG = 1;
    public bool isBoss = false;

    [Header("Runtime stats (set by ScaleToPlayer)")]
    public int enemyHealth;

    [HideInInspector] public int enemyMaxHealth;
    public int maxDMG = 6;
    public int minDMG = 1;
    public int enemyArmour;

    private string objectID;

    void Start()
    {
        enemyMaxHealth = enemyHealth;
        objectID = SpinScript.main.AddIcon(gameObject);
    }

    void Update()
    {

    }

    public void ScaleToPlayer()
    {
        int level = PlayerStats.main.playerLevel;
        int multiplier = isBoss ? 2 : 1;
        enemyHealth = (baseHealth + level * 3) * multiplier;
        maxDMG = (baseMaxDMG + level) * multiplier;
        minDMG = (baseMinDMG + level) * multiplier;
    }

    public void UpdateEnemyHealth(int amount)
    {
        enemyHealth += amount;

        if (enemyHealth <= 0)
        {
            SpinScript.main.RemoveIcon(objectID);
            PlayerStats.main.LevelUp();

            if (isBoss) SceneManager.LoadScene("Victory");
            else Destroy(gameObject);
        }
    }
}

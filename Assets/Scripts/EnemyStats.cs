using System;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int enemyHealth;
    [HideInInspector] public int enemyMaxHealth;
    public int maxDMG = 6;
    public int minDMG = 1;
    public int enemyArmour;
    private string objectID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyMaxHealth = enemyHealth;
        objectID = SpinScript.main.AddIcon(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEnemyHealth(int amount)
    {
        enemyHealth += amount;

        if (enemyHealth <= 0)
        {
            SpinScript.main.RemoveIcon(objectID);

            Destroy(gameObject);
        }
    }
}

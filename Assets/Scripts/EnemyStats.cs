using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int enemyHealth;
    public int maxDMG = 6;
    public int minDMG = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            Destroy(gameObject);
        }
    }
}

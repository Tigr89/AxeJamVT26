using UnityEngine;
using UnityEngine.UI;

public class QuickHPBarScript : MonoBehaviour
{
    public float maxHP;
    public float currentHP;

    [SerializeField] private Image HPBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maxHP = PlayerStats.main.playerMaxHealth;
        currentHP = PlayerStats.main.playerHealth;
        HPBar.fillAmount = currentHP / maxHP;
    }
}

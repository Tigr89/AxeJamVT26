using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatScript : MonoBehaviour
{
    public static CombatScript main;
    public GameObject combatContainer;

    [SerializeField] bool diceRolling;
    public float diceSpeed = 0.1f;

    public Image diceSR;
    [SerializeField] private Sprite[] diceSprites;
    private int diceNumber;

    public GameObject attackTarget;

    public bool inCombat;
    [SerializeField] private bool startCombat; //this bool is used just to trigger the coroutine

    //UI ELEMENTS
    [SerializeField] private TMP_Text playerText;
    [SerializeField] private TMP_Text enemyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (main == null) main = this;
        else Destroy(gameObject);
        combatContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUG
        if (!diceRolling && Input.GetKeyDown(KeyCode.V)) StartCoroutine(CombatDice());

        /*if (startCombat)
        {
            StartCoroutine(Combat());
            startCombat = false;
        }*/


    }

    public IEnumerator CombatDice()
    {
        if (diceRolling) yield break;
        else diceRolling = true;

        while (diceRolling)
        {
            yield return new WaitForSeconds(diceSpeed);
            diceNumber = Random.Range(0, diceSprites.Length);
            diceSR.sprite = diceSprites[diceNumber];
        }

      
        yield return null;
    }

    public IEnumerator Combat()
    {
        inCombat = true;
        combatContainer.SetActive(true);

        while(attackTarget != null)
        {
            EnemyStats enemyStats = attackTarget.GetComponent<EnemyStats>();
            if (playerText != null) playerText.text = "Waiting...";
            if (enemyText != null) enemyText.text = "Waiting...";

            StartCoroutine(CombatDice());
            //PLAYER TURN
            while (diceRolling)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    diceRolling = false;
                }
                yield return null;
            }
            
            if (attackTarget != null) enemyStats.UpdateEnemyHealth(-(diceNumber + PlayerStats.main.playerDamage));
            string playerAttackInfo = "You did " + (diceNumber + 1 + PlayerStats.main.playerDamage) + " damage!";
            if (playerText != null) playerText.text = playerAttackInfo;

            //Kolla om fienden lever
            if (attackTarget == null) break;

            //ENEMY TURN
            int enemyDMG = Random.Range(enemyStats.minDMG, enemyStats.maxDMG) - PlayerStats.main.playerArmour;
            PlayerStats.main.UpdatePlayerHealth(-enemyDMG);
            string enemyAttackInfo = enemyStats.gameObject.name + " did " + enemyDMG + " damage!";
            if (enemyText != null) enemyText.text = enemyAttackInfo;

            Debug.Log(enemyAttackInfo);

            yield return new WaitForSeconds(5);
        }


        yield return new WaitForSeconds(3);

        inCombat = false;
        combatContainer.SetActive(false);

        yield return null;
    }

    public void AddTarget(GameObject target)
    {
        attackTarget = target;
        StartCoroutine(Combat());
    }
}

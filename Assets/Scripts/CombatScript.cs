using System.Collections;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    [SerializeField] bool diceRolling;
    public float diceSpeed = 0.1f;

    public SpriteRenderer diceSR;
    [SerializeField] private Sprite[] diceSprites;
    private int diceNumber;

    public GameObject attackTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUG
        if (!diceRolling && Input.GetKeyDown(KeyCode.V)) StartCoroutine(CombatDice());
        
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

        diceNumber++;

        if (attackTarget != null) attackTarget.GetComponent<EnemyStats>().UpdateEnemyHealth(-(diceNumber + PlayerStats.main.playerDamage));

        yield return null;
    }
}

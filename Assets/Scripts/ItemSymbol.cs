using UnityEngine;

public class ItemSymbol : MonoBehaviour
{
    private Player player;
    public Transform targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExecuteAction()
    {
        //Debug.Log("Symbol " + gameObject.name + " activated!");
        if (targetPosition != null)
        {

            player.NewTarget(targetPosition);

        }

        
    }
}

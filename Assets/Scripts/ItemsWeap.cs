using UnityEngine;

public class ItemsWeap : MonoBehaviour
{
    [SerializeField] SpinScript spinScript;
    private string objectID;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        spinScript = GameObject.Find("SpinLogic").GetComponent<SpinScript>();
        objectID = spinScript.AddIcon(gameObject); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spinScript.RemoveIcon(objectID);
            collision.gameObject.GetComponent<PlayerStats>().playerDamage += 1;
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] SpinScript spinScript;
    private int index;
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
        index = spinScript.AddIcon(gameObject); 
    }
}

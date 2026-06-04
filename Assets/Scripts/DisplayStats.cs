using TMPro;
using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    string[] info;
    public TMP_Text textBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < info.Length; i++)
        {
            textBox.text += info[i] + "\n\n" ;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Code : MonoBehaviour
{
    public int Fnumber;
    public int Snumber;
    public int Tnumber;
    private TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        Fnumber = Random.Range(0, 3);
        Snumber = Random.Range(0, 3);
        Tnumber = Random.Range(0, 3);
        text.text = Fnumber.ToString() + " " + Snumber.ToString() + " " + Tnumber.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

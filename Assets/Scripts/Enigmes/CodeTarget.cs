using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CodeTarget : MonoBehaviour
{
    public Code InitCode;
    public UnityEvent Succeed;
    public int PNumber = 0;
    public int DNumber = 0;
    public int TNumber = 0;
    private TextMeshPro text;

    private void Update()
    {
        text.text = PNumber.ToString() + " " + DNumber.ToString() + " " + TNumber.ToString();
    }
    private void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    public void IncrementP() 
    {
        if (PNumber < 3)
        {
            PNumber++;
        }
        else if (PNumber == 3)
        {
            PNumber = 0;
        }
        Check();
    } 
    
    public void IncrementD() 
    {
        if (DNumber < 3)
        {
            DNumber++;
        }
        else if (DNumber == 3)
        {
            DNumber = 0;
        }
        Check();
    } 
    
    public void IncrementT() 
    {
        if (TNumber < 3)
        {
            TNumber++;
        }
        else if (TNumber == 3)
        {
            TNumber = 0;
        }
        Check();
    }

    public void Check() 
    {
        if (PNumber == InitCode.Fnumber && DNumber == InitCode.Snumber && TNumber == InitCode.Tnumber)
        {
            Succeed.Invoke();
        }
    
    }
}

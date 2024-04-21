using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NumberItem : MonoBehaviour, IContentGetter
{
    [SerializeField] private TMP_Text _numberText;


    public void SetNumberText(int number)
    { 
        _numberText.text = number.ToString();
    }

    public void SetNumberText(string numberText)
    { 
        _numberText.text = numberText;
    }

    public string GetContent()
    {
        return _numberText.text;
    }
}

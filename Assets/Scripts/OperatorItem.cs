using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OperatorItem : MonoBehaviour, IContentGetter
{
    [SerializeField] private TMP_Text _operator;


    public void SetOperatorText(string operatorText)
    { 
        _operator.text = operatorText;
    }

    public string GetContent()
    {
        return _operator.text;
    }
}

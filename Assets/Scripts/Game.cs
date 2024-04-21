using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public event Action OnRightResult;
    [SerializeField] private EquationCreator _equationCreator;
    [SerializeField] private TMP_Text _bottomText;


    private void Start()
    {
        _equationCreator.Create(indexEquation: 0);
        List<GameObject> mathItems = _equationCreator.GetMathItems();
        _equationCreator.OnItemDroppedToSlot += OnItemDropped;

        void OnItemDropped()
        { 
            if (_equationCreator.IsRightResult(mathItems))
            {
                _bottomText.gameObject.SetActive(true);
                OnRightResult?.Invoke();
            }
        }
    }
}

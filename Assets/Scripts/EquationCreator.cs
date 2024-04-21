using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;


public class EquationCreator : MonoBehaviour
{
    public event Action OnItemDroppedToSlot;

    [SerializeField] private GameObject _itemToDragPrefab;
    [SerializeField] private GameObject _itemToDropPrefab;
    [SerializeField] private GameObject _operatorPrefab;
    [SerializeField] private GameObject _itemsToDragParent;
    [SerializeField] private GameObject _itemsToDropParent;

    [SerializeField] private EquationJsonReader _equationsJsonReader;
    [SerializeField] private Canvas _canvas;

    private string _expressionString = "";
    private List<GameObject> _mathItems = new List<GameObject>();


    public void Create(int indexEquation)
    { 
        _expressionString = "";

        EquationJsonReader.EquationList equationList = _equationsJsonReader.GetEquations();

        
        // Get all equations.
        // for (int i = 0; i < equationList.equations.Length; i++)
        // {
        // }

        List<int> numberDigits = new List<int>();

        int id = equationList.equations[indexEquation].id;
        int[] numbers = equationList.equations[indexEquation].numbers;
        string[] operators = equationList.equations[indexEquation].operators;

        for (int j = 0; j < numbers.Length; j++)
        {
            int number = numbers[j];

            int[] numberArray = GetIntArray(number);

            for (int k = 0; k < numberArray.Length; k++)
            {
                int digitOfNumber = numberArray[k];
                numberDigits.Add(digitOfNumber);

                CreateSlot();
            }

            _expressionString += number.ToString();

            if (j < operators.Length)
            {
                CreateOperator(operators[j]);
                _expressionString += operators[j].ToString();
            }
        }

        CreateOperator("=");

        int result = GetResultOfStringExpression(_expressionString);
        int[] resultArray = GetIntArray(result);

        for (int i = 0; i < resultArray.Length; i++)
        {
            int resultDigit = resultArray[i];

            numberDigits.Add(resultDigit);
            CreateSlot();
        }

        CreateRandomNumberItems(numberDigits);


        void CreateRandomNumberItems(List<int> numberDigits)
        {
            List<int> randomDigits = Shuffle<int>(numberDigits);

            for (int i = 0; i < randomDigits.Count; i++)
            {
                CreateNumberItem(randomDigits[i]);
                print(randomDigits[i]);
            }
        }
    }

    public List<GameObject> GetMathItems()
    {
        return _mathItems;
    }

    public bool IsRightResult(List<GameObject> mathItems)
    {
        string expression = "";
        string userResultString = "";
        bool isReultDigit = false;

        for (int i = 0; i < mathItems.Count; i++)
        {
            if (mathItems[i].GetComponent<ItemSlot>() != null && mathItems[i].GetComponent<ItemSlot>().GetContent() == null) return false;

            string mathItemContent = mathItems[i].GetComponent<IContentGetter>().GetContent();
            
            if (mathItemContent == "=")
            {
                isReultDigit = true;
                continue;
            }

            if (isReultDigit == false && mathItemContent != "=") 
            {
                expression += mathItemContent;
            }

            if (isReultDigit)
            {
                userResultString += mathItemContent;
            }
        }

        int trueResult = GetResultOfStringExpression(expression);
        int userResultNumber = GetResultOfStringExpression(userResultString);
        print("trueResult: " + trueResult);
        print("userResultNumber: " + userResultNumber);

        if (userResultNumber == trueResult)
        {
            return true;
        }

        return false;
    }

    
    private void CreateNumberItem(int digitOfNumber)
    {
        GameObject numberItem = Instantiate(_itemToDragPrefab, transform.position, Quaternion.identity, _itemsToDragParent.transform);

        numberItem.GetComponent<DragDrop>().SetCanvas(_canvas);
        numberItem.GetComponent<NumberItem>().SetNumberText(digitOfNumber);

        numberItem.GetComponent<DragDrop>().OnItemDroppedToSlot += OnItemDropped;

        

        void OnItemDropped()
        {
            OnItemDroppedToSlot?.Invoke();
        }
    }

    private void CreateSlot()
    {
        GameObject itemSlot = Instantiate(_itemToDropPrefab, transform.position, Quaternion.identity, _itemsToDropParent.transform);
        _mathItems.Add(itemSlot);
    }

    private void CreateOperator(string operatorSymbol)
    { 
        GameObject operatorItem = Instantiate(_operatorPrefab, transform.position, Quaternion.identity, _itemsToDropParent.transform);

        operatorItem.GetComponent<OperatorItem>().SetOperatorText(operatorSymbol);

        _mathItems.Add(operatorItem);
    }


    private int GetResultOfStringExpression(string expression)
    { 
        return Convert.ToInt32(new DataTable().Compute(expression, ""));
    }

    private int[] GetIntArray(int num)
    {
        List<int> listOfInts = new List<int>();
    
        while (num > 0)
        {
            listOfInts.Add(num % 10);
            num = num / 10;
        }

        listOfInts.Reverse();

        return listOfInts.ToArray();
    }

    private List<T> Shuffle<T>(List<T> original)
    {
        System.Random randomNum = new System.Random();
        int index = 0;
        T temp;
        for (int i = 0; i < original.Count; i++)
        {
            index = randomNum.Next(0, original.Count - 1);
            if (index != i)
            {
                temp = original[i];
                original[i] = original[index];
                original[index] = temp;
            }
        }
        return original;
    }
}

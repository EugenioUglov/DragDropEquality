using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class EquationJsonReader : MonoBehaviour
{
    [System.Serializable]
    public class Equation
    {
        public int id;
        public int[] numbers;
        public string[] operators; 
    }

    [System.Serializable]
    public class EquationList
    {
        public Equation[] equations;
    }

    public EquationList GetEquations()
    {
        string json = File.ReadAllText(Application.dataPath + "/Equations.json");
        EquationList equationList = JsonUtility.FromJson<EquationList>(json);

        return equationList;
    }
}

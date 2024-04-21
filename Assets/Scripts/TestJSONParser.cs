using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJSONParser : MonoBehaviour
{
    public TextAsset textJSON;

    [System.Serializable]
    public class Player
    { 
        public string name;
        public int health;
        public int mana;
    }

    [System.Serializable]
    public class PlayerList
    { 
        public Player[] player; 
    }

    public PlayerList myPlayerList = new PlayerList();

    private void Start()
    {
        myPlayerList = JsonUtility.FromJson<PlayerList>(textJSON.text);
        print(myPlayerList.player[0].name);
    }
}

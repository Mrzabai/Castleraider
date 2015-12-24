using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript manager;
    public int currentMap;
    public int unlocMap;
    public int points;

    void Awake ()
    {
        manager = this;
	}

    public void Save()
    {

    }
}

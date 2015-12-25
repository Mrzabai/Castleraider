using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public bool[] map = new bool[30];
    public bool[] time = new bool[3];
    int numberMap;
    public GameObject gameManager;
    public Canvas firstCanvas;
    public Canvas secondCanvas;
	void Start ()
    {
        //Hittar gameManager, om inte instantiera den
        if (GameObject.Find("GameManager") == null)
        {
            Instantiate(gameManager, new Vector3(0, 0, 0), Quaternion.identity);
            gameManager = GameObject.Find("GameManager");
        }
        DontDestroyOnLoad(gameManager.gameObject);
        //Låser up banor
        for (int i = 0; i > GameManagerScript.manager.unlocMap; i++)
        {
            map[GameManagerScript.manager.unlocMap + i] = true;

        }
        firstCanvas.enabled = true;
        secondCanvas.enabled = false;
        time[0] = true;
        map[0] = true;
    }
    //Välj Tidsepok
    public void PressTimeButton(int number)
    {
        if (time[number])
        {
            numberMap = 10 * number;
            firstCanvas.enabled = false;
            secondCanvas.enabled = true;
        }
    }
    //Välj bana
    public void PressMapButton(int number)
    {
        if (map[number + numberMap])
        {
            GameManagerScript.manager.currentMap = number + numberMap;
            Application.LoadLevel(number + numberMap + 1);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}

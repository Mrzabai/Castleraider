using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainScript : MonoBehaviour {

    float timer;
    int lives;
    int pelletCount;
    public Canvas normalCanvas;
    public Canvas pauseCanvas;
    public Canvas victoryCanvas;
    public Canvas gameoverCanvas;
    public Text pelletText;
    public Text timeText;
    public Text pointsText;
    public Text livesText;
    public Text powerUpText;
    GameObject gameManager;
    bool paused;

    void Start ()
    {
        //Hittar gameManager
        gameManager = GameObject.Find("GameManager");
        DontDestroyOnLoad(gameManager.gameObject);
        //Instantierar Banan
        //Instantiate(maps[GameManagerScript.manager.currentMap]);
        paused = false;
        normalCanvas.enabled = true;
        pauseCanvas.enabled = false;
        victoryCanvas.enabled = false;
        gameoverCanvas.enabled = false;
        lives = 5;
	}
	
	void Update ()
    {
        //Räknar pellets
        pelletCount = GameObject.FindGameObjectsWithTag("Pellet").Length;
        //Räknar tid
        timer += Time.deltaTime;
        //Sätter text
        pelletText.text = pelletCount.ToString();
        timeText.text = timer.ToString();
        pointsText.text = GameManagerScript.manager.points.ToString();
        livesText.text = lives.ToString();
        powerUpText.text = GameManagerScript.manager.currentMap.ToString();

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                Time.timeScale = 1;
                paused = false;
                pauseCanvas.enabled = false;
                normalCanvas.enabled = true;
            }
            else
            {
                paused = true;
                Time.timeScale = 0;
                pauseCanvas.enabled = true;
                normalCanvas.enabled = false;
                
            }
        }

        //Victory!!!
        if (pelletCount <= 0)
        {
            Time.timeScale = 0;
            GameManagerScript.manager.unlocMap = GameManagerScript.manager.currentMap + 1;
            normalCanvas.enabled = false;
            victoryCanvas.enabled = true;
        }

        //GameOver
        if (lives <= 0)
        {
            Time.timeScale = 0;
            normalCanvas.enabled = false;
            gameoverCanvas.enabled = true;
        }
	}

    //Metoder för knappar
    public void QuitMenu()
    {
        Application.LoadLevel(0);
    }
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void NextMap()
    {
        GameManagerScript.manager.currentMap ++;
        Application.LoadLevel(GameManagerScript.manager.currentMap + 1);
    }

    
}

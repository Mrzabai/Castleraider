using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    

    private NavMeshAgent enemyNavAgent;
    Transform pacMan;
    float distanceToPacman;
    public float huntDistance;
    float randomPosX;
    float randomPosZ;
  

    void Update()
    {
        distanceToPacman = Vector3.Distance(gameObject.transform.position, pacMan.position);  //räknar ut avståndet mellan spöket och spelaren (pacman)
        if (distanceToPacman < huntDistance)
        {
            Hunt();
        }
        else
        {
            InvokeRepeating("Patrol", 0f, 3.5f);        //säger åt fienden att patrullera var 3.5:e sekund
        }

        
    }

    void Start()
    {
        pacMan = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyNavAgent = GetComponent<NavMeshAgent>();
        
    }

    void Hunt()
    {
       
        enemyNavAgent.destination = pacMan.position;

    }
    void Patrol()                       //patrullmetoden som gör att fienden i fråga får en ny, slumpad, position att patrullera till
    {
       
        randomPosX = Random.Range(-7.5f, 7.5f);
        randomPosZ = Random.Range(-7.5f, 7.5f);
        enemyNavAgent.destination = new Vector3(randomPosX, 0f, randomPosZ);       
        
    }

    void OnCollisionEnter(Collision col)            // om fienden krockar med en annan fiende så får de en ny punkt att rörasig/patrullera mot
    {
        if(col.gameObject.tag == "Enemy")
        {
           Patrol();
        }
    }

   

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {
    GameObject[] AllPlayerTag;
    List<Vector2> initialPos = new List<Vector2>();
    Vector2 initialBallPos;
    int RedScore = 0;
    int BlueScore = 0;
    public BoostPad BoostPrefab;
    // Use this for initialization
    float Timer = 0;
    void Start() {
        //Assign colours and initial position of players and ball
        GameObject[] AllPlayerTags = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject Player in AllPlayerTags)
        {
            GameObject selectPlayer = Player.transform.parent.gameObject;
            initialPos.Add(Player.transform.parent.position);
            if (selectPlayer.transform.position.x > this.transform.position.x)
            {
                selectPlayer.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                selectPlayer.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
        initialBallPos = GameObject.Find("Ball").transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
        Timer += Time.deltaTime;
        RegisterGoal();
        //create boostpads and destroy them after 10 seconds and respawn new ones
        if(Timer>=0)
        {
            CreateBoostPad();
        }
        if(Timer>=10)
        {
            DestroyBoostPads();
            Timer = 0;
        }
    }

    //print Label of each team score to screen.
    private void OnGUI()
    {

        GUI.Label(new Rect(150, 10, 100, 20), "Blue Score: "+BlueScore);
        GUI.Label(new Rect(850, 10, 100, 20), "Red Score: " + RedScore);


    }

    //reset position of all in game game objects
    private void ResetPos()
    {
        int next = 0;
        GameObject[] AllPlayerTags = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject Player in AllPlayerTags)
        {
            Player.transform.parent.position = initialPos[next];
            next++;
        }
        GameObject.Find("Ball").transform.position = initialBallPos;

    }

    //Register when the ball enters a goal and add score.
    private void RegisterGoal()
    {
        //print("Goal DIstance: " + Vector2.Distance(GameObject.Find("Ball").transform.position, (GameObject.Find("Goal2").transform.position)));
        if(GameObject.Find("Ball").transform.position.x<GameObject.Find("Goal1").transform.position.x && 
           GameObject.Find("Ball").transform.position.y > GameObject.Find("Goal1").transform.position.y-2 && 
           GameObject.Find("Ball").transform.position.y < GameObject.Find("Goal1").transform.position.y + 2)
        {
            RedScore++;
            ResetPos();
        }

        if (GameObject.Find("Ball").transform.position.x > GameObject.Find("Goal2").transform.position.x &&
           GameObject.Find("Ball").transform.position.y > GameObject.Find("Goal2").transform.position.y - 2 &&
           GameObject.Find("Ball").transform.position.y < GameObject.Find("Goal2").transform.position.y + 2)
        {
            BlueScore++;
            ResetPos();
        }
    }

    private BoostPad CreateBoostPad()
    {


        GameObject[] BoostPads = GameObject.FindGameObjectsWithTag("Boost");
        int Count = 0;
        foreach (GameObject Boost in BoostPads)
        {
            GameObject BPCheck = Boost.transform.gameObject;
            Count++;
        }
        if (Count < 1) { 
        //Invoke("setAliveState", 10);
        BoostPad BP = Instantiate(BoostPrefab) as BoostPad;

        //assign name to power-up
        BP.name = "Boost Pad";
        BP.transform.parent = transform;
        //set the vector 3 position of the power-up
        BP.transform.localPosition = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.5f),-2);
        return BP;
         }
        else
        {
            
            
            return null;
        }
    }

    //method to destroy boostpads
    private void DestroyBoostPads()
    {
        GameObject[] BoostPads = GameObject.FindGameObjectsWithTag("Boost");
       
        foreach (GameObject Boost in BoostPads)
        {
            GameObject BPCheck = Boost.transform.gameObject;
            Destroy(BPCheck);
        }
    }


}

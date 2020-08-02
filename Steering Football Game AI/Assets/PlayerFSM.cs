using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour {
    public enum State {Alive,Dead,Shoot,Boost}
    public State currentState = State.Alive;
    public enum Agent { Steer,Context,Flock,Wander};
    public Agent agentType;
    public GameObject[] BoostPads;
    public float Timer = 0;
    // Use this for initialization
    void Start () {
        //Assign the agent based on what script is attatched to each agent
     if(this.gameObject.GetComponent<Steering>()!=null)
        {
            agentType = Agent.Steer;
        }
        if (this.gameObject.GetComponent<Flock>() != null)
        {
            agentType = Agent.Flock;
        }
        if (this.gameObject.GetComponent<Context_Steering>() != null)
        {
            agentType = Agent.Context;
        }
        if (this.gameObject.GetComponent<WanderSteer>() != null)
        {
            agentType = Agent.Wander;
        }
    }
	
	// Update is called once per frame
	void Update () {
        BoostPads = GameObject.FindGameObjectsWithTag("Boost");
        FSM(agentType);
	}

    void FSM(Agent Type)
    {
        if(Type==Agent.Steer)
        {
            SteerFSM();
        }
        if (Type == Agent.Context)
        {
            ContextFSM();
        }
        if (Type == Agent.Flock)
        {
            FlockFSM();
        }
        if (Type == Agent.Wander)
        {
            WanderFSM();
        }
    }

    //FSM for normal seek steering agent
    void SteerFSM()
    {
        switch (currentState)
        {
            //Normal state: performs normal seeking behaviour
            case State.Alive:
                //  Steering.maxV = 0.07f;
                Steering script = this.gameObject.GetComponent<Steering>();
                script.maxV = 0.07f;
                if (Vector2.Distance(this.transform.position, GameObject.Find("Ball").transform.position) < 0.5)
                {
                    currentState = State.Shoot;
                }

                foreach (GameObject Boost in BoostPads)
                {
                    GameObject BPCheck = Boost.transform.gameObject;
                    if (Vector2.Distance(this.transform.position, BPCheck.transform.position) < 0.5)
                    {
                        currentState = State.Boost;
                        Destroy(BPCheck);
                    }
                }

                break;
                //Shoot state: Change target to goal when the object is close to the ball.
            case State.Shoot:
                script = this.gameObject.GetComponent<Steering>();
                if (this.GetComponent<Renderer>().material.color == Color.blue)
                {
                    script.UpdatedV= (GameObject.Find("Goal1").transform.position) - (this.transform.position);
                }
                else
                {
                    script.UpdatedV = (GameObject.Find("Goal2").transform.position) - (this.transform.position);
                }
                foreach (GameObject Boost in BoostPads)
                {
                    GameObject BPCheck = Boost.transform.gameObject;
                    if (Vector2.Distance(this.transform.position, BPCheck.transform.position) < 0.5)
                    {
                        currentState = State.Boost;
                        Destroy(BPCheck);
                    }
                }
                break;
                //increase max velocity of the agent
            case State.Boost:
                script = this.gameObject.GetComponent<Steering>();
                script.maxV = 0.1f;
                Timer += Time.deltaTime;
                if(Timer>=10)
                {
                    setAliveState();

                }
                break;
        }
    }

    void ContextFSM()
    {
        switch (currentState)
        {
            case State.Alive:
                Context_Steering script = this.gameObject.GetComponent<Context_Steering>();
                script.maxV = 0.04f;
                foreach (GameObject Boost in BoostPads)
                {
                    GameObject BPCheck = Boost.transform.gameObject;
                    if (Vector2.Distance(this.transform.position, BPCheck.transform.position) < 0.5)
                    {
                        currentState = State.Boost;
                        Destroy(BPCheck);
                    }
                }
                break;
          
            case State.Boost:

                script = this.gameObject.GetComponent<Context_Steering>();
                script.maxV = 0.1f;
                Timer += Time.deltaTime;
                if (Timer >= 10)
                {
                    setAliveState();

                }
                break;
        }
    }

    void FlockFSM()
    {
        switch (currentState)
        {
            case State.Alive:
                
                Flock.maxV = 0.05f;
                foreach (GameObject Boost in BoostPads)
                {
                    GameObject BPCheck = Boost.transform.gameObject;
                    if (Vector2.Distance(this.transform.position, BPCheck.transform.position) < 0.5)
                    {
                        currentState = State.Boost;
                        Destroy(BPCheck);
                    }
                }
                break;

            case State.Boost:
                Flock.maxV = 0.07f;
                Timer += Time.deltaTime;
                if (Timer >= 10)
                {
                    setAliveState();

                }
                break;
        }
    }

    void WanderFSM()
    {
        switch (currentState)
        {
            case State.Alive:
                WanderSteer script = this.gameObject.GetComponent<WanderSteer>();
                script.maxV= 0.05f;
                foreach (GameObject Boost in BoostPads)
                {
                    GameObject BPCheck = Boost.transform.gameObject;
                    if (Vector2.Distance(this.transform.position, BPCheck.transform.position) < 0.5)
                    {
                        currentState = State.Boost;
                        Destroy(BPCheck);
                    }
                }
                break;

            case State.Boost:
                script = this.gameObject.GetComponent<WanderSteer>();
                script.maxV = 0.1f;
                Timer += Time.deltaTime;
                if (Timer >= 10)
                {
                    setAliveState();

                }
                break;
        }
    }

    //set state to alive.
    private void setAliveState()
    {
        Timer = 0;
        currentState = State.Alive;
    }


}

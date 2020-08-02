using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context_Steering : MonoBehaviour {
    GameObject[] interests = new GameObject[10];
    float[] distanceIntensity = new float[10];
    Vector2 P;
    Vector2 InitialP;
    Vector2 InitialVelocity;
    Vector2 UpdatedP;
    Vector2 Steer;
    GameObject Ball;
    GameObject P1;
    Vector2 Goal;
    float lowest;
    int next = 0;
    Vector2 Velocity;

    public float maxV = 0.04f;
    // Use this for initialization
    void Start () {
        Ball = GameObject.Find("Ball");
        P1 = GameObject.Find("Player1");
        P = this.transform.position;
        InitialP = this.transform.position;
        //populate interest map
        interests[0] = (this.gameObject);
        interests[1] = (Ball);
        //interests[2] = (P1);
        
   
	}
	
	// Update is called once per frame
    //Create a goal keeper agent which attempts to stop incomming interests if their distance is within 5 and always returns to goal
	void Update () {
        UpdatedP = this.transform.position;

        PlayerFSM script = this.gameObject.GetComponent<PlayerFSM>();

       
            GameObject[] BoostPads = GameObject.FindGameObjectsWithTag("Boost");
            for (int i = 2, j = 0; j < BoostPads.Length; i++, j++)
            {
                interests[i] = BoostPads[j];

            if (script.currentState == PlayerFSM.State.Boost)
            {

                interests[i] = null;
            }


        }

        if (script.currentState != PlayerFSM.State.Boost)
        {
         
        }
        //populate danger map consiting of the distances of all the interests
        for (int i=0;i<ArrayEleNo(interests);i++)
        {
           
            distanceIntensity[i] = Vector2.Distance(interests[i].transform.position, InitialP);
            //if i = 0 then set the distance in the danger map at 0 to be the distance from its current position and initial position
            if(i==0)
            {
                //lowest = least dangerous an interest
               lowest = Vector2.Distance(InitialP, UpdatedP);
                distanceIntensity[i] = lowest;
                next = i;
            }
            //if any interests are within a distance of 5 then seek them
            else if (distanceIntensity[i]<=5 && i!=0 && distanceIntensity[0]<=5 )
            {
                lowest = Vector2.Distance(interests[i].transform.position, UpdatedP);
                next = i;
            }
            //  print(Vector2.Distance(interests[i], UpdatedP) < lowest || distanceIntensity[i] < 5 && distanceIntensity[0] < 5);

        }
      //  print( "Distance: "+Vector2.Distance(Ball.transform.position,this.transform.position));
            Velocity = (Vector3to2(interests[next].transform.position) - UpdatedP);
        //if all interests are beyond a distance of 5 then return to initial position
        if(next==0 )
        {
            
            Velocity = (InitialP - UpdatedP);

 
        }
            Velocity.Normalize();
            Velocity *= maxV;
            P += Velocity;
            this.transform.position = P;
      
        
    }

    //convert a Vector3 to a Vector2
    Vector2 Vector3to2(Vector3 v)
    {
        Vector2 xy = new Vector2(v.x,v.y);
        return xy;
    }

    int ArrayEleNo(Object[] array)
    {
        int r = 0;
        foreach(Object o in array)
        {
            if (o != null)
            {
                r++;
            }
        }
        return r;
    }
}

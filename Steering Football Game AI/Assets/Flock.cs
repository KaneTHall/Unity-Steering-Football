using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {
    // Use this for initialization
    //Followed Psudocode on how to implement Flock behaviour here: http://www.kfish.org/boids/pseudocode.html
    Vector2 v;
    Vector2 P;
    public static float maxV = 0.05f;
    void Start () {
		foreach(GameObject player in GameObject.FindGameObjectsWithTag("Flock"))
        {
           // print(player.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
        mainFlock();
	}



    void mainFlock()
    {
        Vector2 vel1;
        Vector2 vel2;
        Vector2 vel3;
        P = this.transform.position;
        v = (Vector3to2(GameObject.Find("Ball").transform.position) - P);
        vel1 = rule1(this.gameObject);
        vel2 = rule2(this.gameObject);
        vel3 = rule3(this.gameObject);
        v += (vel1 + vel2 + vel3);
        v.Normalize();
        v *= maxV;
        P += v;
       this.transform.position = P;
       
        //   print(" Vel1:" + vel1 + " Vel2:" + vel2 + " Vel3:" + vel3);
    }

    Vector2 rule1(GameObject b)
    {
        //initialize vector position of center
        Vector2 center;
        center = new Vector2(0, 0);
        //initialize N (number of boids in flock)
        int N=0;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Flock"))
        {
            //calculate percieved center of flock
            if(player!=b)
            {
                center += Vector3to2(player.transform.position);
               
            }
            N++;
            
        }

        center = center/(N-1);
       //move boids 1% towards the center
        return (center - Vector3to2(b.transform.position)) / 100;

    }

    Vector2 rule2(GameObject b)
    {
        Vector2 c = new Vector2(0, 0);

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Flock"))
        {
            //subtract the displacement of each boid that is near by with a distance of less than 0.8
            if (player != b)
            {
                if(Vector2.Distance(player.transform.position,b.transform.position)<0.8)
                {
                  
                    
                    c -= (Vector3to2(player.transform.position) - Vector3to2(b.transform.position));
                }
            }
        }
        //return vector that moves away from boids
        return c;
    }


    Vector2 rule3(GameObject b)
    {
        Vector2 perceivedV = new Vector2(0, 0);
        int N = 0;
        //accumalte all velocity vectors for all boids and find the average boid velocity
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Flock"))
        {
            if (player != b)
            {
                perceivedV += v;
              
            }
            N++;
        }
        perceivedV = (perceivedV) / (N - 1);
        //print(perceivedV-v);
        return (perceivedV - v) / 8;

    }

        Vector2 Vector3to2(Vector3 v)
    {
        Vector2 xy = new Vector2(v.x, v.y);
        return xy;
    }
}

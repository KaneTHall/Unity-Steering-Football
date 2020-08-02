using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderSteer : MonoBehaviour {

    // Use this for initialization
    Vector2 circlePos;
   public float circleDist;
    public float circleRad;
    public float maxV = 0.05f;
    Vector2 velocity;
    Vector2 target;
    Vector2 randomDisplacement;
    Vector2 centerCircle;
    float Angle;
    float Timer = 0;
    void Start () {
        velocity = Rotate(new Vector2(0, 0.00001f), Random.Range(0, 360));
        //initialise angle
        Angle = Random.Range(0, 360);
    }
	
	// Update is called once per frame
	void Update(){
       //set current position of agent to P
        Vector2 P = this.transform.position;
        //Add the steering vectors to P
        P += WanderCircle();
        //update P position
        this.transform.position = P;


	}

    Vector2 WanderCircle()
    {

        
        //initialise a circle at the head of the agent.
        centerCircle = this.transform.position;
        circlePos = centerCircle+velocity*circleDist;
        circlePos.Normalize();
        //initialise timer
        Timer += Time.deltaTime;
        float x = 0.5f;
        //change direction of the vector every x seconds 
        if (Timer >= x)
        {
            Angle = Random.Range(0, 360);
            Timer = 0;
        }
        //set a target at the edge of the wander circle
            target = circlePos + Rotate(new Vector2(circleRad, 0), Angle);
        //calculate steering force to get to target vector.
        Vector2 desiredV = target - centerCircle;
        desiredV.Normalize();
        desiredV *= maxV;
        Vector2 wanderCircle = (desiredV - velocity);
        return wanderCircle;
      
    }


    //method to convert Vector 3 to Vector 2
    Vector2 Vector3to2(Vector3 v)
    {
        Vector2 xy = new Vector2(v.x, v.y);
        return xy;
    }


    //method to rotate a 2D vector
    public  Vector2 Rotate(Vector2 v, float d)
    {
        return Quaternion.Euler(0, 0, d) * v;
    }
}

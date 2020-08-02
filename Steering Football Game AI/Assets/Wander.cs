using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class Wander : MonoBehaviour {

    Vector2 velocity;
    public float circleDistance = 1.5f;
    public float circleRadius;
    public float Angle;
    Vector2 P;
    // Use this for initialization
    void Start () {
        velocity = (GameObject.Find("Wall Top)").transform.position) - this.transform.position;
        circleRadius = circleDistance / 2;
        Angle = 10;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 Steer = WarderSteer();
        velocity *= 0.02f;
        velocity += Steer;
        P += velocity;
        this.transform.position = P;
    }

    Vector2 WarderSteer()
    {
        //find the center of the circle.
         P = this.transform.position;
        Vector2 scaleD = new Vector2(circleDistance, circleDistance);
        P.Normalize();
        P.Scale(scaleD);
        Vector2 randomDisplacement = new Vector2(0, 1);
        Vector2 scaleR = new Vector2(circleRadius, circleRadius);
        randomDisplacement.Scale(scaleR);
        WAngle(randomDisplacement, Angle);
        Angle += Random.Range(0, 1.0f) *(500-30)*0.5f;
        Vector2 WanderF;
        WanderF = P + randomDisplacement;
        
        return WanderF;
    }

    void WAngle(Vector2 v, float A)
    {
        v.x = Mathf.Cos(A) * v.magnitude;
        v.y = Mathf.Sin(A) * v.magnitude;
    }


}
*/
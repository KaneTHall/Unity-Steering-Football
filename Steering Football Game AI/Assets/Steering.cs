using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//seek steering
public class Steering : MonoBehaviour {
    Vector2 Velocity;
    Vector2 P;
    public Vector2 Target;
    Vector2 Steer;
    public Vector2 UpdatedV;
    Vector2 UpdatedT;
    Vector2 UpdatedP;
    public float maxV=0.07f;
    // Use this for initialization
    void Start () {
        print(this.transform.position);
        //set target and current position
        P = this.transform.position;
        Target = GameObject.Find("Ball").transform.position;
        //calculate Vector with direction towards target
        Velocity = (Target - P);
        Velocity.Normalize();
        UpdatedV.Normalize();
    }
	
	// Update is called once per frame
	void Update () {
        //calculate current velocity from current position and target
        UpdatedP = this.transform.position;
        UpdatedT = GameObject.Find("Ball").transform.position;
        UpdatedV = (UpdatedT - UpdatedP);
        UpdatedV.Normalize();
        UpdatedV *= maxV;
        //CAlculate the steering force 
        Steer = (UpdatedV - Velocity);
        Velocity += Steer;

        P += Velocity;
        this.transform.position = P;
       
	}
}

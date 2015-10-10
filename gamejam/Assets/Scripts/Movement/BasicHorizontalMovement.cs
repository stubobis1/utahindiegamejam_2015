using UnityEngine;
using System.Collections;

public class BasicHorizontalMovement : Movement {

    public float maxLeftMovement = 25.0f;
    public float maxRightMovement = 25.0f;
    float leftBoundary, rightBoundary;
    bool directionSwitchReady = true;
    
    public override void Start () {
        speed = 5.0f;

        leftBoundary = transform.position.x - maxLeftMovement;
        rightBoundary = transform.position.x + maxRightMovement;

        base.Start();
	}
	
	void Update () {
        if (directionSwitchReady &&
            transform.position.x < leftBoundary || transform.position.x > rightBoundary)
        {
            direction *= -1;
            directionSwitchReady = false;
        }

        if(!directionSwitchReady &&
            (transform.position.x > leftBoundary || transform.position.x < rightBoundary))
            directionSwitchReady = true;

        GetComponent<Rigidbody2D>().velocity = speed * direction;
	}
}

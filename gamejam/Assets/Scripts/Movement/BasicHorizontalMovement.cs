using UnityEngine;
using System.Collections;

public class BasicHorizontalMovement : EnemyMovement {

    public float maxLeftMovement = 25.0f;
    public float maxRightMovement = 25.0f;
    float leftBoundary, rightBoundary, directionSwitchCooldown;
    bool directionSwitchReady = true;
    
    public override void Start () { 
        leftBoundary = transform.position.x - maxLeftMovement;
        rightBoundary = transform.position.x + maxRightMovement;

        base.Start();
	}

    public override void Update() {
        if (directionSwitchReady && directionSwitchCooldown <= 0.0f && (transform.position.x < leftBoundary || transform.position.x > rightBoundary))
        {
            direction *= -1;
            directionSwitchReady = false;
        }

        if (!directionSwitchReady && directionSwitchCooldown <= 0.0f &&
            transform.position.x > leftBoundary && transform.position.x < rightBoundary)
            directionSwitchCooldown = 1.0f;

        if (directionSwitchCooldown > 0)
        {
            directionSwitchCooldown -= Time.deltaTime;
            if (directionSwitchCooldown <= 0.0f)
                directionSwitchReady = true;
        }

        base.Update();
	}
}

using UnityEngine;
using System.Collections;

public class VerticalOscillationMovement : EnemyMovement {

    public bool ascending = true;
    public float maxUpMovement = 5.0f;
    public float maxDownMovement = 5.0f;
    public float oscillatingSpeed = 5.0f;
    public float oscillationShiftTime = 1.0f;
    float topBoundary, bottomBoundary, directionSwitchCooldown;
    bool directionSwitchReady = false;
    
    public override void Start () {
        topBoundary = transform.position.y + maxUpMovement;
        bottomBoundary = transform.position.y - maxDownMovement;

        base.Start();
	}

    public override void Update() {
        //if (directionSwitchReady && directionSwitchCooldown <= 0.0f &&
        //    (transform.position.y > topBoundary || transform.position.y < bottomBoundary))
        //{
        //    ascending = !ascending;
        //    directionSwitchReady = false;
        //}

        //if (!directionSwitchReady && directionSwitchCooldown <= 0.0f &&
        //    transform.position.y < topBoundary && transform.position.y > bottomBoundary)
        //    directionSwitchCooldown = 1.0f;

        //if (directionSwitchCooldown > 0)
        //{
        //    directionSwitchCooldown -= Time.deltaTime;
        //    if (directionSwitchCooldown <= 0.0f)
        //        directionSwitchReady = true;
        //}

        if(ascending && transform.position.y < topBoundary)
        {
            transform.position += oscillatingSpeed * new Vector3(0, 1, 0) * Time.deltaTime;

            directionSwitchCooldown = oscillationShiftTime;
        }
        else if(!ascending && transform.position.y > bottomBoundary)
        {
            transform.position += oscillatingSpeed * new Vector3(0, 1, 0) * Time.deltaTime;

            directionSwitchCooldown = oscillationShiftTime;
        }
        

        base.Update();
	}
}

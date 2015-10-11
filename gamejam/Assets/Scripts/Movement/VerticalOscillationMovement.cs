using UnityEngine;
using System.Collections;

public class VerticalOscillationMovement : EnemyMovement {

    public bool ascending = true;
    public float maxUpMovement = 5.0f;
    public float maxDownMovement = 5.0f;
    public float oscillatingSpeed = 5.0f;
    public float oscillationPauseTime = 1.0f;
    float oscillationDelay = 0.0f;
    bool shifting = false;
    public float directionSwitchPauseTime = 1.0f;
    float directionSwitchCooldown = 0.0f;
    float topBoundary, bottomBoundary;

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

        if(oscillationDelay <= 0.0f)
        {
            if (ascending && transform.position.y < topBoundary)
                transform.position += oscillatingSpeed * new Vector3(0, 1, 0) * Time.deltaTime;
            else if (!ascending && transform.position.y > bottomBoundary)
                transform.position -= oscillatingSpeed * new Vector3(0, 1, 0) * Time.deltaTime;

            if (directionSwitchCooldown <= 0.0f && (transform.position.y >= topBoundary || transform.position.y <= bottomBoundary))
                oscillationDelay = oscillationPauseTime;                
        }

        if (oscillationDelay > 0.0f)
        {
            oscillationDelay -= Time.deltaTime;

            if(oscillationDelay <= 0.0f)
            {
                ascending = !ascending;
                directionSwitchCooldown = directionSwitchPauseTime;
            }    
        }

        if (directionSwitchCooldown > 0)
            directionSwitchCooldown -= Time.deltaTime;
            

        base.Update();
	}
}

using UnityEngine;
using System.Collections;

public class HomingMovement : EnemyMovement {
    private GameObject target;

    public bool peekaboo = false;
    float peekabooThreshold = 60.0f;
    bool chasing = false;
    bool targetAcquired = false;
    float retargetTimer = 0.0f;
    Vector2 cachedVelocity;

	public override void Start () {
        base.Start();

        speed = 2.0f;
	}
	
	public override void Update () {
        if (!targetAcquired)
        {
            target = gameObject.GetComponent<Enemy>().target;

            if (target)
                targetAcquired = true;
        }
            

        if (peekaboo && target)
        {
            Vector3 playerDirection = target.GetComponent<PlayerControl>().facingRight ?
                new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);

            if (Vector3.Dot(playerDirection, (target.transform.position - transform.position).normalized) < 0)
                disabled = true;
            else
                disabled = false;
        }

        if (!disabled)
        {
            if (target && retargetTimer <= 0.0f)
            {
                retargetTimer += 1.0f;
                Vector3 targetVector = (target.transform.position - gameObject.transform.position).normalized;
                direction = new Vector2(targetVector.x, targetVector.y);
            }
            else if (!target)
                direction = new Vector2();

            if (retargetTimer > 0.0f)
                retargetTimer -= Time.deltaTime;

            GetComponent<Rigidbody2D>().AddForce(direction * speed);
        }
	}

    public override void Enable()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = cachedVelocity;
        retargetTimer = 0.0f;
        base.Enable();
    }

    public override void Disable()
    {
        Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
        cachedVelocity = body.velocity;
        body.velocity = new Vector2();
        base.Disable();
    }
}

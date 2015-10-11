using UnityEngine;
using System.Collections;

public class HomingMovement : EnemyMovement {
    private GameObject target;

    public bool peekaboo = false;
    float peekabooThreshold = 60.0f;
    bool chasing = false;
    public float maxHomingDistance = 200.0f;
    public float maxSightDistance = 200.0f;
    bool targetAcquired = false;
    float retargetTimer = 0.0f;
    public float retargetDelay = 0.5f;
    Vector2 cachedVelocity;

	public override void Start () {
        base.Start();

        if (peekaboo)
            disabled = true;
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

            Vector3 toPlayer = target.transform.position - transform.position;

            if (Vector3.Dot(playerDirection, toPlayer.normalized) < 0 &&
                toPlayer.magnitude <= maxSightDistance)
                disabled = true;
            else
                if(toPlayer.magnitude < maxHomingDistance)
                    disabled = false;
        }

        if (!disabled)
        {
            if (target && retargetTimer <= 0.0f)
            {
                retargetTimer = retargetDelay;
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

    public override void FixedUpdate()
    {
        if(target && !disabled)
        {
            float boundsLength = Mathf.Max(GetComponent<Collider2D>().bounds.extents.x,
                GetComponent<Collider2D>().bounds.extents.y);
            float targetBoundsLength = Mathf.Max(target.GetComponent<Collider2D>().bounds.extents.x,
                target.GetComponent<Collider2D>().bounds.extents.y);

            if ((transform.position - target.transform.position).magnitude <=
                boundsLength + targetBoundsLength)
                target.BroadcastMessage("Death");
        }

        base.FixedUpdate();
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

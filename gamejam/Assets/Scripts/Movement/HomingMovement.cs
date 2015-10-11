using UnityEngine;
using System.Collections;

public class HomingMovement : EnemyMovement {
    private GameObject target;

    float retargetTimer = 0.0f;
    bool targetAcquired = false;
    Vector2 cachedVelocity;

	public override void Start () {
        base.Start();

        speed = 2.0f;
	}
	
	public override void Update () {
        if(!disabled)
        {
            if (!targetAcquired)
                this.target = gameObject.GetComponent<Enemy>().target;

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

    public override void Enable(string layer)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = cachedVelocity;
        retargetTimer = 0.0f;
        base.Enable(layer);
    }

    public override void Disable(string layer)
    {
        cachedVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        base.Disable(layer);
    }
}

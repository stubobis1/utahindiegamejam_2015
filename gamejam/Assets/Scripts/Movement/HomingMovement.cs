using UnityEngine;
using System.Collections;

public class HomingMovement : EnemyMovement {
    private GameObject target;

	public override void Start () {
        speed = 0.5f;

        this.target = gameObject.GetComponent<Enemy>().target;

        base.Start();
	}
	
	public override void Update () {
        GetComponent<Rigidbody2D>().AddForce(direction * speed);
	}
}

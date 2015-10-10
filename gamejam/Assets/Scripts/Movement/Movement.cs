using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float speed = 1.0f;
    public Vector2 direction = new Vector2(1, 0);
    public bool flying = false;
    protected Animator anim;

    public virtual void Start () {
        if(flying)
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = speed * direction;
    }
}

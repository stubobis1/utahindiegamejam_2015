using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float speed = 1.0f;
    public Vector2 direction = new Vector2(1, 0);
    public bool flying = false;
    protected Animator anim;
    protected bool disabled = false;

    public virtual void Start () {
        anim = GetComponent<Animator>();

        if(flying)
            GetComponent<Rigidbody2D>().gravityScale = 0.0f;
	}
	
	public virtual void Update () {
        GetComponent<Rigidbody2D>().velocity = speed * direction;
    }

    public virtual void FixedUpdate() {
        if (!disabled)
            anim.SetFloat("Speed", Mathf.Abs(speed));
        else
            anim.SetFloat("Speed", 0);
    }

    public virtual void Enable()
    {
        disabled = false;
    }

    public virtual void Enable(int layer) {
        
    }

    public virtual void Disable() {
        disabled = true;
    }
}

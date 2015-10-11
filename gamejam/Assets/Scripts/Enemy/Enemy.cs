using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    [HideInInspector]
    public GameObject target;
    public bool facingRight = false;
    public bool ghost = false;

    public enum ZoneType {
        None,
        Front,
        Back,
        Both
    }

    public ZoneType zoneType = ZoneType.None;

    void Start () {
        if (zoneType == ZoneType.None)
            this.gameObject.SetActive(false);

        target = GameObject.FindGameObjectWithTag("Player");

        if (ghost)
            this.gameObject.layer = LayerMask.NameToLayer("Ghost");
    }
	
	public virtual void Update () {

    }

    public virtual void FixedUpdate() {
        if (GetComponent<Rigidbody2D>().velocity.x > 0 && !facingRight)
            Flip();
        else if (GetComponent<Rigidbody2D>().velocity.x < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Switch(int layer)
    {
        switch(zoneType)
        {
            case ZoneType.Front:
                if (LayerMask.LayerToName(layer) == "Front")
                    Activate();
                else
                    Deactivate();
                break;
            case ZoneType.Back:
                if (LayerMask.LayerToName(layer) == "Back")
                    Activate();
                else
                    Deactivate();
                break;
            case ZoneType.Both:
                Activate(layer);

                if(this.gameObject.layer != LayerMask.NameToLayer("Ghost"))
                    this.gameObject.layer = layer;
                break;
            default:
                this.gameObject.SetActive(false);
                break;
        }
    }

    public virtual void Activate()
    {
        this.gameObject.GetComponent<EnemyMovement>().Enable();
    }

    public virtual void Activate(int layer) {
        this.gameObject.GetComponent<EnemyMovement>().Enable(layer);
    }

    public virtual void Deactivate(){
        this.gameObject.GetComponent<EnemyMovement>().Disable();
    }

    public virtual void OnCollisionEnter2D(Collision2D collision) {            
        if (collision.gameObject.tag == "Player")
            collision.gameObject.BroadcastMessage("Death");
    }

    public virtual void Death()
    {
        Destroy(this.gameObject);
    }
}

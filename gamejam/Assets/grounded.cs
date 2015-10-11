using UnityEngine;
using System.Collections;

public class grounded : MonoBehaviour {

    [HideInInspector]
    public bool isGrounded = false;

	void Start () {
	
	}
	
	void FixedUpdate() {
        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, new Vector2(0, -1),
            gameObject.GetComponent<Collider2D>().bounds.extents.y + 0.8f, 1 << gameObject.layer);

        if (hits.Length <= 0)
            isGrounded = false;

        foreach(RaycastHit2D rh in hits)
        {
            if (rh.transform.tag == "Ground")
            {
                isGrounded = true;
                break;
            }
            else
                isGrounded = false;
        }
	}
}

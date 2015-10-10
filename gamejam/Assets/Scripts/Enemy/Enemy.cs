using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [HideInInspector]
    public GameObject target;

    public bool frontSwitch = false;
    public bool backSwitch = false;

    void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
	}
	
	public virtual void Update () {

    }

    void Switch(string layer)
    {
        if (frontSwitch && layer == "Front")
        {
            Debug.Log(gameObject.name + ": " + "FRONT");
            this.gameObject.layer = LayerMask.NameToLayer("Front");
        }
            
        else if (backSwitch && layer == "Back")
        {
            Debug.Log(gameObject.name + ": " + "BACK");
            this.gameObject.layer = LayerMask.NameToLayer("Front");
        }    
    }

    public virtual void Activate(string layer) {

    }

    public virtual void OnCollisionEnter(Collision collision) {

    }
}

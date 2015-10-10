using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public bool frontSwitch = false;
    public bool backSwitch = false;
    public GameObject target;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

    }

    void Switch(string layer)
    {
        if (frontSwitch && layer == "Front")
        {
            Debug.Log(gameObject.name + ": " + "FRONT");
            //this.gameObject.layer = 
        }
            
        else if (backSwitch && layer == "Back")
            Debug.Log(gameObject.name + ": " + "BACK");
    }

    public virtual void Activate(string layer)
    {

    }

    public virtual void OnCollisionEnter(Collision collision)
    {

    }
}

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    [HideInInspector]
    public GameObject target;

    public bool frontSwitch = false;
    public bool backSwitch = false;

    void Start () {
        if (!frontSwitch & !backSwitch)
            this.gameObject.SetActive(false);

        target = GameObject.FindGameObjectWithTag("Player");
	}
	
	public virtual void Update () {

    }

    public void Switch(int layer)
    {
        if (frontSwitch && layer == LayerMask.NameToLayer("Front"))
        {
            Activate("Front");
        }
        else if (backSwitch && layer == LayerMask.NameToLayer("Back"))
        {
            Activate("Back");
        }

        this.gameObject.layer = layer;
    }

    public virtual void Activate(string layer) {
        this.gameObject.GetComponent<EnemyMovement>().Enable(layer);
    }

    public virtual void Deactivate(string layer){
        this.gameObject.GetComponent<EnemyMovement>().Disable(layer);
    }

    public virtual void OnCollisionEnter(Collision collision) {

    }
}

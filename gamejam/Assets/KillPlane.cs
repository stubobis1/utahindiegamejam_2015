using UnityEngine;
using System.Collections;

public class KillPlane : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider2D col)
    {
        if( col.gameObject.tag == "Player")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}

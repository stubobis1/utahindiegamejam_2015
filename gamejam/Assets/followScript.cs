using UnityEngine;
using System.Collections;

public class followScript : MonoBehaviour {

    public Transform transformToFollow;
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(transformToFollow.position.x, transformToFollow.position.y);
	}
}

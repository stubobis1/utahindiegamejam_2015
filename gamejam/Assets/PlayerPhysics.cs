using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour {
    public void Move(Vector2 moveAmount){
        transform.Translate(moveAmount);
        }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

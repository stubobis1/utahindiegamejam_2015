using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject frontZone;
    public GameObject backZone;

	void Awake () {
        if (frontZone == null || backZone == null)
            Debug.LogError("ZONES MISSING IN GAME MANAGER");

        foreach(Transform t in frontZone.transform)
        {
            GameObject go = t.gameObject;
            go.layer = LayerMask.NameToLayer("Front");
            go.GetComponent<SpriteRenderer>().sortingLayerName = "FrontSort";
        }

        foreach(Transform t in backZone.transform)
        {
            GameObject go = t.gameObject;
            go.layer = LayerMask.NameToLayer("Back");
            go.GetComponent<SpriteRenderer>().sortingLayerName = "BackSort";
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

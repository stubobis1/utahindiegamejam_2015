using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject frontZone;
    public GameObject backZone;

	void Awake () {
        if (frontZone == null || backZone == null)
            Debug.LogError("ZONES MISSING IN GAME MANAGER");

        SpriteRenderer[] zoneChildren = frontZone.GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer sr in zoneChildren)
        {
            if(sr.GetInstanceID() != frontZone.GetInstanceID())
            {
                sr.gameObject.layer = LayerMask.NameToLayer("Front");
                sr.sortingLayerName = "FrontSort";
            }
        }

        zoneChildren = backZone.GetComponentsInChildren<SpriteRenderer>();

        foreach(SpriteRenderer sr in zoneChildren)
        {
            if (sr.GetInstanceID() != frontZone.GetInstanceID())
            {
                sr.gameObject.layer = LayerMask.NameToLayer("Back");
                sr.sortingLayerName = "BackSort";
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

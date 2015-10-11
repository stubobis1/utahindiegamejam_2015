using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject frontZone;
    public GameObject backZone;

	void Awake () {
        if (frontZone == null || backZone == null)
            Debug.LogError("ZONES MISSING IN GAME MANAGER");

        SpriteRenderer[] frontZoneChildren = frontZone.GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer[] backZoneChildren = backZone.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in frontZoneChildren)
        {
            if(sr.GetInstanceID() != frontZone.GetInstanceID())
            {
                sr.gameObject.layer = LayerMask.NameToLayer("Front");
                sr.sortingLayerName = "FrontSort";
            }
        }

        

        foreach(SpriteRenderer sr in backZoneChildren)
        {
            if (sr.GetInstanceID() != frontZone.GetInstanceID())
            {
                sr.gameObject.layer = LayerMask.NameToLayer("Back");
                sr.sortingLayerName = "BackSort";
                sr.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

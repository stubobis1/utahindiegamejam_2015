using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject frontZone;
    public GameObject backZone;

    [HideInInspector]
    public zoneScript backZoneScript;
    [HideInInspector]
    public zoneScript frontZoneScript;

    void Awake () {
        if (frontZone == null || backZone == null)
            Debug.LogError("ZONES MISSING IN GAME MANAGER");

        SpriteRenderer[] frontZoneChildren = frontZone.GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer[] backZoneChildren = backZone.GetComponentsInChildren<SpriteRenderer>();

        backZone.tag = "Zone";
        frontZone.tag = "Zone";
        frontZoneScript = frontZone.GetComponent<zoneScript>();
        backZoneScript = backZone.GetComponent<zoneScript>();
        backZoneScript.isFront = false;
        frontZoneScript.isFront = true;

        foreach (SpriteRenderer sr in frontZoneChildren)
        {
            if(sr.GetInstanceID() != frontZone.GetInstanceID())
            {
                sr.gameObject.layer = LayerMask.NameToLayer("Front");
                if (sr.transform.tag == "Background")
                {
                    sr.sortingLayerName = "BackSort";
                }
                else
                {
                    sr.sortingLayerName = "FrontSort";
                }
            }
        }

        

        foreach(SpriteRenderer sr in backZoneChildren)
        {
            if (sr.GetInstanceID() != frontZone.GetInstanceID())
            {
                if (sr.transform.tag == "Background")
                {
                    sr.color = Color.clear;
                }
                else
                {
                    sr.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                }
                sr.gameObject.layer = LayerMask.NameToLayer("Back");
                sr.sortingLayerName = "BackSort";
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

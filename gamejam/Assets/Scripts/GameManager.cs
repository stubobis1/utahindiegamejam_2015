using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject zone1;
    public GameObject zone2;

	void Awake () {
        if (zone1 == null || zone2 == null)
            Debug.LogError("ZONES MISSING IN GAME MANAGER");

        SpriteRenderer[] spriteRenderers = zone1.GetComponentsInChildren<SpriteRenderer>();

        Debug.Log(spriteRenderers.Length);

        foreach (SpriteRenderer s in spriteRenderers)
            s.sortingLayerName = "Front";

        spriteRenderers = zone2.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer s in spriteRenderers)
            s.sortingLayerName = "Back";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

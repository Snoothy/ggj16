using UnityEngine;
using System.Collections.Generic;

public class RandomSittingGuest : MonoBehaviour {

    public List<Sprite> allSprites = new List<Sprite>();
    public SpriteRenderer spr;

	// Use this for initialization
	void Start () {
        spr.sprite = allSprites[Random.Range(0, allSprites.Count-1)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

	public float offset = 0;

	Transform player, t;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		t = transform;
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion q = Quaternion.LookRotation (t.position - player.position);
		t.rotation = Quaternion.Euler(new Vector3(0f, q.eulerAngles.y + offset, 0f));
	}
}

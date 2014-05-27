using UnityEngine;
using System.Collections;

public class LevelMove : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
	}
}

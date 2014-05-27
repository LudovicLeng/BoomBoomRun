using UnityEngine;
using System.Collections;

public class DestroyBomb : MonoBehaviour {

	
	public GameObject Explosion;
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player")
		{
			Destroy(this.gameObject);
			Instantiate( Explosion, this.gameObject.transform.position, Quaternion.identity );
		}
		
	}
}

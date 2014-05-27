using UnityEngine;
using System.Collections;

public class CameraAxeY : MonoBehaviour {

	public GameObject player;

	// Update is called once per frame
	void Update () {
	
		this.gameObject.transform.position = new Vector3(player.gameObject.transform.position.x + 9,this.gameObject.transform.position.y,this.gameObject.transform.position.z);

	}

}

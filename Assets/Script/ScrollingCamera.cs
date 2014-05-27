using UnityEngine;
using System.Collections;

public class ScrollingCamera : MonoBehaviour {

	public int SpeedScrolling = 1;

	void Update() {
		transform.Translate(Vector3.right * Time.deltaTime * SpeedScrolling, Camera.main.transform);
	}
}
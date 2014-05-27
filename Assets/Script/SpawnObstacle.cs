using UnityEngine;
using System.Collections;

public class SpawnObstacle : MonoBehaviour {
	
	public GameObject obstacle;
	float x = 1;
	private GameObject obstacleDetect;

	void Start(){
		GameObject obstacleDetect = GameObject.FindGameObjectWithTag("LevelMap");
		Instantiate(obstacle, new Vector3(x * 180.6699f, -5.465f , -1.0f),Quaternion.identity);
	}

	void Update () {

	}
}
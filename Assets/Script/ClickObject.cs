using UnityEngine;
using System.Collections;

public class ClickObject : MonoBehaviour
{
	Ray ray;
	RaycastHit hit;
	public GameObject bomb;
	
	void Update()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit))
		{
			Instantiate( bomb, hit.point, Quaternion.identity );
			print (hit.collider.name);
		}
	}
}
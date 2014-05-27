
using UnityEngine;
using System.Collections;

public class ClickObject : MonoBehaviour
{
	Ray ray;
	RaycastHit hit;
	public GameObject bomb;
	public float maxBomb = 3;
	private int frames = 0;
	
	void Update()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit) && Input.GetMouseButtonUp(0))
		{
			if(maxBomb > 0)
			{
				Instantiate( bomb, hit.point, Quaternion.identity );
				maxBomb -=1;
			}
		}

		++frames;

		if(frames == 90){
			if(maxBomb <=3)
				maxBomb +=1;
			frames = 0;
		}


	}
}
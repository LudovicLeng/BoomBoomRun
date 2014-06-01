using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    delegate void MoveUpdate();

	Ray ray;
	RaycastHit hit;

    public CharacterController controller;
    public float groundSpeed = 20;
    public float gravity = 3;
    public float groundJump = 50;
    public float airSpeed = 1;
    public float wallGravity = 1.5f;
   public float wallJump = 50;
   public float wallSpeed = 25;

    private Dictionary<CollisionFlags, MoveUpdate> moveUpdate_;
    private Vector3 motion_;
    private ControllerColliderHit lastHit_;
    private Vector3 oldPositionOfLastHit_;

    void Awake()
    {
        if (this.controller == null) 
        {
            this.controller = this.GetComponent<CharacterController>();
            if (this.controller == null)
                Debug.LogError("Player Controller n'a pas de charactere controller !");
        }
        this.moveUpdate_ = new Dictionary<CollisionFlags, MoveUpdate>();
        this.moveUpdate_[CollisionFlags.Below] = this.GroundUpdate;
        this.moveUpdate_[CollisionFlags.Sides] = this.WallUpdate;
        this.moveUpdate_[CollisionFlags.None] = this.AirUpdate;
        this.moveUpdate_[CollisionFlags.Above] = this.AirUpdate;
        this.motion_ = Vector3.zero;
    }

	void Update()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit) && Input.GetMouseButton(0))
		{
			print (hit.collider.name);
		}
	}

	
	void LateUpdate() 
    {
        this.motion_ = Vector3.zero;
        if (this.moveUpdate_.ContainsKey(this.controller.collisionFlags))
            this.moveUpdate_[this.controller.collisionFlags]();
        else
            this.moveUpdate_[CollisionFlags.Below]();
        this.controller.Move(this.motion_ * Time.deltaTime);
        if (this.lastHit_ != null)
            this.oldPositionOfLastHit_ = this.lastHit_.transform.position;
	}

    void GroundUpdate()
    {
        this.controller.transform.Translate(this.lastHit_.transform.position - this.oldPositionOfLastHit_);
        this.motion_.y = -this.gravity * Time.deltaTime;
       /* if (Input.GetKey(KeyCode.LeftArrow)) 
            this.motion_.x = -this.groundSpeed;
        if (Input.GetKey(KeyCode.RightArrow)) */
            this.motion_.x = this.groundSpeed;
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
		
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit) && hit.collider.name == "Player" && Input.GetMouseButton(0))
		{
            this.motion_.y = this.groundJump;
		}
    }

    void WallUpdate()
    {
        this.controller.transform.Translate(this.lastHit_.transform.position - this.oldPositionOfLastHit_);
        this.motion_ = this.lastHit_.moveDirection ;
        this.motion_.y = -this.wallGravity * Time.deltaTime;
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit) && hit.collider.name == "Player" && Input.GetMouseButton(0))
		{
            this.motion_ = ((this.lastHit_.moveDirection * -0.5f) + Vector3.up).normalized * this.wallJump;
		}
        if (Input.GetKey(KeyCode.DownArrow)) 
            this.motion_.y = -this.wallSpeed;
    }

    void AirUpdate()
    {
        this.lastHit_ = null;
        this.motion_ = this.controller.velocity - (this.controller.velocity * Time.deltaTime);
        this.motion_.y -= this.gravity * Time.deltaTime;
        /*if (Input.GetKey(KeyCode.LeftArrow)) 
            this.motion_.x += -this.airSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow)) */
            this.motion_.x += this.airSpeed * Time.deltaTime;
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit) && hit.collider.name == "Player" && Input.GetMouseButton(0))
		{
			this.motion_.y = this.groundJump;
		}
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        this.lastHit_ = hit;
    }
}
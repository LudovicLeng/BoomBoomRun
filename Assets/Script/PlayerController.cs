using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    delegate void MoveUpdate();

	Ray ray;
	RaycastHit hit;
	private bool CanJump = false;
	private bool CanDie = false;

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

	void OnGUI()
	{
		if (CanDie)
		{
			GUI.Label(new Rect(10 ,10 ,100,100), "You die !");
			if (GUI.Button(new Rect (10, 30, 100,50),"Retry"))
			{
				Application.LoadLevel("Model");
			}
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
		if(CanJump)
		{
            this.motion_.y = this.groundJump;
		}
    }

    void WallUpdate()
    {
        this.controller.transform.Translate(this.lastHit_.transform.position - this.oldPositionOfLastHit_);
        this.motion_ = this.lastHit_.moveDirection ;
        this.motion_.y = -this.wallGravity * Time.deltaTime;
		if(CanJump)
		{
            this.motion_ = ((this.lastHit_.moveDirection * -0.5f) + Vector3.up).normalized * this.wallJump;
			CanJump = false;
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
		if(CanJump)
		{
			this.motion_.y = this.groundJump;
			CanJump = false;
		}
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        this.lastHit_ = hit;
    }

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Bomb")
		{
			CanJump = true;	
		}
		if (other.tag == "DeathZone")
		{
			CanDie = true;	
		}
		
	}
}
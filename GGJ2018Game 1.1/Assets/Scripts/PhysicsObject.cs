using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    private float minGroundNormalY = 0.85f;
    public float gravityModifier = 1f;
	protected Vector2 gravity;
	protected Vector2 groundNormal;
	private int invertedX = 1;
	private int invertedY = -1;
	private Vector2 UpVector;

	protected Vector2 targetVelocity;
    protected bool grounded;    
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.03f;

	protected bool isAgainstWallL = false;
	protected bool isAgainstWallR = false;
	protected Vector2 currentForwardNormal;

	public enum GravitySide
	{
		Down,
		Left,
		Up,
		Right
	}

	public GravitySide side;

	void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    void Start () 
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
        contactFilter.useLayerMask = true;
    }
    
    void Update () 
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity (); 
    }

    protected virtual void ComputeVelocity()
    {
    
    }

	public void SwapGravity()
	{
		switch(side)
		{
			case GravitySide.Up:
				{
					gravity = new Vector2(0, 9.8f);
					invertedY = -1;
					UpVector = new Vector2(0, 1);
				}
				break;
			case GravitySide.Down:
				{
					gravity = new Vector2(0, -9.8f);
					invertedY = 1;
					UpVector = new Vector2(0, 1);
				}
				break;
			case GravitySide.Left:
				{
					gravity = new Vector2(-9.8f, 0);
					invertedY = 1;
					UpVector = new Vector2(1, 0);
				}
				break;
			case GravitySide.Right:
				{
					gravity = new Vector2(9.8f, 0);
					UpVector = new Vector2(1, 0);
				}
				break;
		}
	}

    void FixedUpdate()
    {
	    //velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		velocity += gravityModifier * gravity * Time.deltaTime;

		if (side == GravitySide.Down || side == GravitySide.Up)
		{
			velocity.x = targetVelocity.x;
		}
		else if(side == GravitySide.Left || side == GravitySide.Right)
		{
			velocity.y = targetVelocity.y;
		}

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

		//Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
		Vector2 moveAlongGround = new Vector2 (groundNormal.y * invertedY, -groundNormal.x * invertedX);

		Vector2 move = Vector2.zero;
		if (side == GravitySide.Up || side == GravitySide.Down)
		{
			move = moveAlongGround * deltaPosition.x;
		}
		else if (side == GravitySide.Left || side == GravitySide.Right)
		{
			move = moveAlongGround * deltaPosition.y;
		}

		Movement (move, false);

		if (side == GravitySide.Up || side == GravitySide.Down)
		{
			move = UpVector * deltaPosition.y;
		}
		else if (side == GravitySide.Left || side == GravitySide.Right)
		{
			move = UpVector * deltaPosition.x;
		}

		Movement (move, true);
	}

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
       if (distance > minMoveDistance) 
        {
			int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear ();
            for (int i = 0; i < count; i++) {
                hitBufferList.Add (hitBuffer [i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++) 
            {
                Vector2 currentNormal = hitBufferList [i].normal;

				DetectGround(currentNormal, yMovement);

				float projection = Vector2.Dot (velocity, currentNormal);
                if (projection < 0) 
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList [i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

	void DetectGround(Vector2 currentNormal, bool yMovement)
	{
		switch(side)
		{
			case GravitySide.Up:
				{
					if (currentNormal.y < minGroundNormalY)
					{
						grounded = true;
						if (yMovement)
						{
							groundNormal = currentNormal;
							currentNormal.x = 0;
						}
					}
				}
				break;
			case GravitySide.Down:
				{
					if (currentNormal.y > minGroundNormalY)
					{
						grounded = true;
						if (yMovement)
						{
							groundNormal = currentNormal;
							currentNormal.x = 0;
						}
					}
				}
				break;
			case GravitySide.Left:
				{
					if (currentNormal.x > minGroundNormalY)
					{
						grounded = true;
						if (yMovement)
						{
							groundNormal = currentNormal;
							currentNormal.y = 0;
						}
					}
				}
				break;
			case GravitySide.Right:
				{
					if (currentNormal.x < minGroundNormalY)
					{
						grounded = true;
						if (yMovement)
						{
							groundNormal = currentNormal;
							currentNormal.y = 0;
						}
					}
				}
				break;
		}
	}

}

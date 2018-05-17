using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;

	public AudioSource audioSource;

	protected float jumpTakeOffSpeed = 10;

	private float waitForRotationTime = 0.2f;
	private bool hasSwaptLeft = false;
	private bool hasSwaptRight = false;

	public SkeletonAnimation skeletonAnimation;

    private bool playingFallingAnimation = false;

	private bool playingWalkcycle = false;

	// Use this for initialization
	void Awake () 
    {
		//animator = GetComponent<Animator> ();

		side = GravitySide.Down;
		SwapGravity();

		StartCoroutine(ComputeVelocity2());

        StartCoroutine(checkFallingAnimation());
    }


    IEnumerator checkFallingAnimation()
    {
        // Falling Animation check loop
        while (true)
        {
            if(!grounded && !playingFallingAnimation)
            {
                playingFallingAnimation = true;
                skeletonAnimation.AnimationName = "Fall";
            }

            else if (grounded)
            {
                playingFallingAnimation = false;
                //skeletonAnimation.AnimationName = "Idle";
            }
            yield return null;
        }
    }


	IEnumerator ComputeVelocity2()
	{
		while (true)
		{
			if (hasSwaptLeft)
			{
				switch (side)
				{
					case GravitySide.Up:
						{
							if (grounded)
							{
								velocity.y = -jumpTakeOffSpeed;
								yield return new WaitForSeconds(waitForRotationTime);
							}
						}
						break;
					case GravitySide.Down:
						{
							if (grounded)
							{
								velocity.y = jumpTakeOffSpeed;
								yield return new WaitForSeconds(waitForRotationTime);
							}
						}
						break;
					case GravitySide.Left:
						{							
							if (grounded)
							{
								velocity.x = jumpTakeOffSpeed;
								// left right is flipped for side
								yield return new WaitForSeconds(waitForRotationTime);
							}
						}
						break;
					case GravitySide.Right:
						{
							if (grounded)
							{
								velocity.x = -jumpTakeOffSpeed;
								yield return new WaitForSeconds(waitForRotationTime);
							}
						}
						break;
				}
				SetNextState();
				transform.Rotate(0, 0, -90);
				hasSwaptLeft = false;

			}

			if(hasSwaptRight)
			{

				switch (side)
				{
					case GravitySide.Up:
						{
							if (grounded)
							{
								velocity.y = -jumpTakeOffSpeed;
								yield return new WaitForSeconds(waitForRotationTime);
							}
						}
						break;
					case GravitySide.Down:
						{
							if (grounded)
							{
								velocity.y = jumpTakeOffSpeed;
								yield return new WaitForSeconds(waitForRotationTime);
							}
						}
						break;
					case GravitySide.Left:
						{
							if (grounded)
							{
								velocity.x = jumpTakeOffSpeed;
								yield return new WaitForSeconds(waitForRotationTime);
							}
						}
						break;
					case GravitySide.Right:
						{
							if (grounded)
							{
								velocity.x = -jumpTakeOffSpeed;
								yield return new WaitForSeconds(waitForRotationTime);
							}
						}
						break;
				}

				SetPreviousState();
				transform.Rotate(0, 0, 90);
				hasSwaptRight = false;
			}

			yield return null;
		}
	}

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

		if (side == GravitySide.Up || side == GravitySide.Down)
		{
			move.x = Input.GetAxis("Horizontal");

			if (move.x != 0 && !playingWalkcycle && grounded)
			{
				skeletonAnimation.AnimationName = "Walkcycle";
				playingWalkcycle = true;
			}
			else if (move.x == 0 && grounded)
			{
				playingWalkcycle = false;
				skeletonAnimation.AnimationName = "Idle";
			}
		}
		else if (side == GravitySide.Left || side == GravitySide.Right)
		{
			move.y = Input.GetAxis("Horizontal");

			if (move.y != 0 && !playingWalkcycle && grounded)
			{
				playingWalkcycle = true;
				skeletonAnimation.AnimationName = "Walkcycle";
			}
			else if (move.y == 0 && grounded)
			{
				playingWalkcycle = false;
				skeletonAnimation.AnimationName = "Idle";
			}
		}


		//if (Input.GetButtonDown ("Jump") && grounded) {
		////if(	Input.GetButtonDown("SwapLeft") && grounded){
		//
		//		velocity.y = jumpTakeOffSpeed * 1000;
		//} else if (Input.GetButtonUp ("Jump")) 
		//{
		//    if (velocity.y > 0) {
		//      //  velocity.y = velocity.y * 0.5f;
		//    }
		//}

		if (Input.GetButtonDown("SwapLeft"))
		{
			hasSwaptLeft = true;
			audioSource.Play();
		}

		if (Input.GetButtonDown("SwapRight"))
		{
			hasSwaptRight = true;
			audioSource.Play();
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			side = GravitySide.Up;
			SwapGravity();
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			side = GravitySide.Down;
			SwapGravity();

		}
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			side = GravitySide.Left;
			SwapGravity();

		}
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			side = GravitySide.Right;
			SwapGravity();

		}

		FlipSprite(move);

				//animator.SetBool ("grounded", grounded);
				// animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

		targetVelocity = move * maxSpeed;
    }

	void SetNextState()
	{
		int current = (int)side;
		current++;
		if (current >= 4)
		{
			current = 0;
		}
		side = (GravitySide)current;
		SwapGravity();
	}

	void SetPreviousState()
	{
		int current = (int)side;
		current--;
		if (current < 0)
		{
			current = 3;
		}
		side = (GravitySide)current;
		SwapGravity();
	}

	void FlipSprite(Vector2 move)
	{
			switch(side)
			{
			case GravitySide.Down:
				if (move.x > 0.01f)
				{
					skeletonAnimation.skeleton.FlipX = false;
				}
				else if (move.x < -0.01f)
				{
					if (skeletonAnimation.skeleton.FlipX == false)
					{
						skeletonAnimation.skeleton.FlipX = true;
					}
				}
				break;
			case GravitySide.Up:
				if (move.x > 0.01f)
				{
					if (skeletonAnimation.skeleton.FlipX == false)
					{
						skeletonAnimation.skeleton.FlipX = true;
					}
				}
				else if (move.x < -0.01f)
				{
					if (skeletonAnimation.skeleton.FlipX == true)
					{
						skeletonAnimation.skeleton.FlipX = false;
					}
				}
				break;
			case GravitySide.Left:
				if (move.y > 0.01f)
				{
					if (skeletonAnimation.skeleton.FlipX == true)
					{
						skeletonAnimation.skeleton.FlipX = false;
					}
				}
				else if (move.y < -0.01f)
				{
					if (skeletonAnimation.skeleton.FlipX == false)
					{
						skeletonAnimation.skeleton.FlipX = true;
					}
				}
				break;
			case GravitySide.Right:
				if (move.y > 0.01f)
				{
					if (skeletonAnimation.skeleton.FlipX == true)
					{
						skeletonAnimation.skeleton.FlipX = false;
					}
				}
				else if (move.y < -0.01f)
				{
					if (skeletonAnimation.skeleton.FlipX == false)
					{
						skeletonAnimation.skeleton.FlipX = true;
					}
				}
				break;
		}
	}

}
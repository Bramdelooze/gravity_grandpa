using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour 
{
	public GameObject[] checkpoints;
	public GameObject startPoint;

	// Use this for initialization
	void Start () {

	}

	public void RespawnAtCheckpoint(GameObject player)
	{
		int lastAchievedCheckpoint = -1;
		for(int i = 0; i < checkpoints.Length; i++)
		{
			CheckPoint checkpoint = checkpoints[i].GetComponent<CheckPoint>();
			if (checkpoint.hasBeenChecked)
				lastAchievedCheckpoint = i;
		}
/*
 0=down
90=right
-180=up
 -90=left
 */
		if(lastAchievedCheckpoint < 0)
		{
			// we havent achieved any checkpoints yet, respawn at startpoint
			player.transform.position = startPoint.transform.position;
			return;
		}

		CheckPoint.Orientation orientation = checkpoints[lastAchievedCheckpoint].GetComponent<CheckPoint>().orientation;

		PlayerPlatformerController controller = player.GetComponent<PlayerPlatformerController>();
		controller.side =  (PhysicsObject.GravitySide)orientation;
		controller.SwapGravity();

		switch(orientation)
		{
			case CheckPoint.Orientation.Down:
				//player.transform.Rotate(0, 0, 0);
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
			case CheckPoint.Orientation.Left:
				//player.transform.Rotate(0, 0, -90);
                player.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
			case CheckPoint.Orientation.Right:
				//player.transform.Rotate(0, 0, 90);
                player.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
			case CheckPoint.Orientation.Up:
				//player.transform.Rotate(0, 0, -180);
                player.transform.rotation = Quaternion.Euler(0, 0, -180);
                break;
		}

		player.transform.position = checkpoints[lastAchievedCheckpoint].transform.position;
	}
}

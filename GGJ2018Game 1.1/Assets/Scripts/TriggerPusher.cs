using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPusher : MonoBehaviour {
    
    public float pushForce = 0.75f;
    public enum BlastDirection
    {
        N,
        E,
        S,
        W
    }
    public BlastDirection blastDirection;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch(blastDirection)
        {
            case BlastDirection.N:
                collision.gameObject.transform.position += new Vector3(0, pushForce, 0);
                break;
            case BlastDirection.E:
                collision.gameObject.transform.position += new Vector3(pushForce, 0, 0);
                break;
            case BlastDirection.S:
                collision.gameObject.transform.position += new Vector3(0, -pushForce, 0);
                break;
            case BlastDirection.W:
                collision.gameObject.transform.position += new Vector3(-pushForce, 0, 0);
                break;
        }
        /*if(blastDirection == 1)
        {
            collision.gameObject.transform.position += new Vector3(-pushForce, 0, 0);
        } else if(blastDirection == 2)
        {
            collision.gameObject.transform.position += new Vector3(0, pushForce, 0);
        } else if(blastDirection == 3)
        {
            collision.gameObject.transform.position += new Vector3(pushForce, 0, 0);
        } else if(blastDirection == 4)
        {
            collision.gameObject.transform.position += new Vector3(0, -pushForce, 0);
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public enum Direction
    {
        N,
        NE,
        E,
        SE,
        S,
        SW,
        W,
        NW
    }

    [HideInInspector]
    public Direction shootDirection;
    [HideInInspector]
    public float moveSpeed;

	public GameObject BloodParticle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        switch(shootDirection)
        {
            case Direction.N:
                transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
                break;
            case Direction.NE:
                transform.position += new Vector3(moveSpeed * Time.deltaTime, moveSpeed * Time.deltaTime, 0);
                break;
            case Direction.E:
                transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                break;
            case Direction.SE:
                transform.position += new Vector3(moveSpeed * Time.deltaTime, -moveSpeed * Time.deltaTime, 0);
                break;
            case Direction.S:
                transform.position += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
                break;
            case Direction.SW:
                transform.position += new Vector3(-moveSpeed * Time.deltaTime, -moveSpeed * Time.deltaTime, 0);
                break;
            case Direction.W:
                transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
                break;
            case Direction.NW:
                transform.position += new Vector3(-moveSpeed * Time.deltaTime, moveSpeed * Time.deltaTime, 0);
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.name == "Player")
        {
			Instantiate(BloodParticle, collision.gameObject.transform.position, Quaternion.identity);
			collision.gameObject.GetComponent<PlayerTileManager>().mainGameManager.GetComponent<MainGameManager>().hasDied = true;
			
		}
    }
}

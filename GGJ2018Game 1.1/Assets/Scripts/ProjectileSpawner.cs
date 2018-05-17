using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour {

    public float projectileSecondsInterval = 2.0f;
    public float speed = 2.0f;

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
    public Direction direction;

    public GameObject projectile;

	// Use this for initialization
	void Start () {
        // InvokeRepeating("Shoot", 0f, projectileInterval);
        StartCoroutine(Shoot());
	}
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator Shoot()
    {
        while(true)
        {
            projectile.GetComponent<ProjectileMove>().moveSpeed = speed;
            projectile.GetComponent<ProjectileMove>().shootDirection = (ProjectileMove.Direction)direction;

            switch(direction)
            {
                case Direction.E:
                case Direction.W:
                    Instantiate(projectile, transform.position, (transform.rotation * Quaternion.Euler(0, 0, 90)));
                    break;
                case Direction.N:
                case Direction.S:
                    Instantiate(projectile, transform.position, (transform.rotation * Quaternion.Euler(0, 0, 0)));
                    break;
                case Direction.NE:
                case Direction.SW:
                    Instantiate(projectile, transform.position, (transform.rotation * Quaternion.Euler(0, 0, -45)));
                    break;
                case Direction.SE:
                case Direction.NW:
                    Instantiate(projectile, transform.position, (transform.rotation * Quaternion.Euler(0, 0, 45)));
                    break;
            }
            yield return new WaitForSeconds(projectileSecondsInterval);
        }

    }
}

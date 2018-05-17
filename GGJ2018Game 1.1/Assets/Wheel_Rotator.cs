using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel_Rotator : MonoBehaviour {

    [SerializeField]
    private float rotation;

    [SerializeField]
    private float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        rotation += rotationSpeed;

        transform.rotation = Quaternion.Euler(0, 0, rotation);

	}
}

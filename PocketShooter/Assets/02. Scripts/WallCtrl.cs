using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCtrl : MonoBehaviour {

	public GameObject sparkEffect;

	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.tag == "Ball") {

			//GameObject spark = (GameObject) Instantiate (sparkEffect, coll.transform.position, Quaternion.identity);

			//Destroy (spark, spark.GetComponent<ParticleSystem> ().duration + 0.2f);

			Destroy (coll.gameObject);
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

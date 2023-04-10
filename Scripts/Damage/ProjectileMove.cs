using UnityEngine;
using System.Collections;
using Mirror;

public class ProjectileMove : MonoBehaviour {

	public float Speed = 100;
	void Start () {
		GameObject.Destroy(this.gameObject,2);
	}
	
	void FixedUpdate () {
		this.transform.position += this.transform.forward * Speed * Time.fixedDeltaTime;
	}
}

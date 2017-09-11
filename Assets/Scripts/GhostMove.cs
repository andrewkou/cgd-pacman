using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour {

	public Transform[] waypoints; 
	int current = 0;
	public GameManager gameManager; 
	public float speed = 0.3f;

	void FixedUpdate()
	{
		if (transform.position != waypoints [current].position) {
			Vector2 p = Vector2.MoveTowards (transform.position, waypoints [current].position, speed); 
			GetComponent<Rigidbody2D> ().MovePosition (p);
		} else {
			current = (current + 1) % waypoints.Length;
		}

		Vector2 dir = waypoints [current].position - transform.position;
		GetComponent<Animator> ().SetFloat ("DirX", dir.y);
		GetComponent<Animator> ().SetFloat ("DirY", dir.y);
	}



	void OnTriggerEnter2D(Collider2D co)
	{
		if (co.name == "pacman") {
			Destroy (co.gameObject); 
			gameOver();
			print ("game over"); 
		}
	}

	void gameOver(){
		//Application.LoadLevel (Application.loadedLevel);
		FindObjectOfType<GameManager>().EndGame();
		print ("game over"); 
	}
}

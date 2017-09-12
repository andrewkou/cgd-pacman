using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour {

	private GameObject[] dots;

	public float speed = 0.4f;
	Vector2 dest = Vector2.zero;

	// Use this for initialization
	void Start () {
		dest = transform.position;
		dots = new GameObject[328];
		GameObject[] obj = FindObjectsOfType(typeof(GameObject)) as GameObject[];
		var c = 0;
		for (var i=0; i < obj.Length; i++) {
			if (obj[i].name.Contains("pacdot"))
			{
				dots [c] = obj[i];
				c++;
			}
		}

		if (c == 328)
			print ("we gucci");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 p = Vector2.MoveTowards (transform.position, dest, speed);
		GetComponent<Rigidbody2D> ().MovePosition (p);

		if ((Vector2)transform.position == dest) {
			if (Input.GetKey (KeyCode.UpArrow) && valid(Vector2.up))
				dest = (Vector2)transform.position + Vector2.up;
			if (Input.GetKey (KeyCode.RightArrow) && valid(Vector2.right))
				dest = (Vector2)transform.position + Vector2.right;
			if (Input.GetKey (KeyCode.DownArrow) && valid(-Vector2.up))
				dest = (Vector2)transform.position - Vector2.up;
			if (Input.GetKey(KeyCode.LeftArrow) && valid(-Vector2.right))
				dest = (Vector2)transform.position - Vector2.right;
		}

		Vector2 dir = dest - (Vector2)transform.position;
		GetComponent<Animator> ().SetFloat ("DirX", dir.x);
		GetComponent<Animator> ().SetFloat ("DirY", dir.y);
	}

	bool valid(Vector2 dir) {
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast (pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D>());
	}

	void OnTriggerEnter2D(Collider2D co)
	{
		if (allGone() == true) {
			gameOver ();
			print ("you done boi");
		}
	}

	bool allGone()
	{
		for (int i=0; i<dots.Length;i++)
		{
			if(dots[i] != null)
			{
				return false;
			}
		}
		return true;
	}
	void gameOver(){
		//Application.LoadLevel (Application.loadedLevel);
		FindObjectOfType<GameManager>().EndGame();
		print ("game over"); 
	}

}

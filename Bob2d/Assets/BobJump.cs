using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobJump : MonoBehaviour {

	public float speed;
	public float jump;
	bool grounded = false;
	private float move;
	private bool isFacingRight = true;
	private Animator anim;
	private Rigidbody2D rb2d;


	void Start ()
	{
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update ()
	{
		//Прыгай маленький ублюдок!)
		if (Input.GetKeyDown (KeyCode.Space) ||
		    Input.GetKeyDown (KeyCode.W)) {
			if (grounded) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jump);
			}
		}
		
		//Бегаем
		float move = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(move));
		rb2d.velocity = new Vector2(move * 1, rb2d.velocity.y);
		if (move > 0 && !isFacingRight)
			Flip();
		else if (move < 0 && isFacingRight)
			Flip();
	}



	private void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	//Проверка на земле или нет
	void OnTriggerEnter2D()
	{
			grounded = true;
	}

	void OnTriggerExit2D()
	{
			grounded = false;
	}

	//Мочим ублюдка
	//void OnColliderEnter2D (Collider2D col)
//if (col.gameObject.name == "enemy") {
//	Destroy (col.gameObject);
		
			//col.gameObject.GetComponent<AudioSource>().Play();
			//col.gameObject.GetComponent<Animator>().Play(NameAnimation);
			//score++;
			//Destroy(col.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
		}

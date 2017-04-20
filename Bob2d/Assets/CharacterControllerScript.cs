using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterControllerScript : MonoBehaviour
{
    public float maxSpeed = 12f;
    public float delay = 0f;
    public int score;
    public string NameAnimation;
    private bool isFacingRight = true;
    private Animator anim;
    private Rigidbody2D rb2d;
    private float jumpForce = 5F;
	public Texture2D icon;
	public GUIStyle style;
	public  string sceneName;
	public string GameObject;


	[SerializeField]
	private GameObject gameOverUI;

    private bool isGrounded = false;


    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();


    }
    private void FixedUpdate()
    {
        CheckGround();
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        rb2d.velocity = new Vector2(move * 3, rb2d.velocity.y);
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

    private void Jump()
    {
        rb2d.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }


    void OnTriggerEnter2D (Collider2D col)
      {
          if (col.gameObject.name == "coin")
          {
            col.gameObject.GetComponent<AudioSource>().Play();
            col.gameObject.GetComponent<Animator>().Play(NameAnimation);
            score++;
            Destroy(col.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
		}
			if (col.gameObject.name == "deadzone")
			{
			gameOverUI.SetActive (true);
			//	SceneManager.LoadScene (sceneName);
			}

         
      }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);

        isGrounded = colliders.Length > 1;

    }

    void OnGUI ()
    {
		GUI.skin.box = style;
		GUI.Box(new Rect (110, 22, 45, 20), " " + score);
		GUI.Box(new Rect (20, 20, 64, 64), icon);


    }
}
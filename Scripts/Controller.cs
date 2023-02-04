using UnityEngine;

public class Controller : MonoBehaviour {
	public float speed = 2f;
	public float gravity = -22f;

	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;

	bool isGrounded;

	[SerializeField] CharacterController characterController;
	[SerializeField] Transform direction;
	[SerializeField] SpriteRenderer spriteRenderer;

    public Sprite[] CharacterArray;

	Vector3 move;
	Vector3 velocity;
	//private Vector3 rotation;

	
    private void ChangeSprite(int count)
    {
        spriteRenderer.sprite = CharacterArray[count]; 
        float angleY = direction.rotation.eulerAngles.y;
		this.transform.rotation = Quaternion.Euler(0f, angleY, 0f);
    }

	private void Update() {	

		float Horizontal = Input.GetAxis("Horizontal");
		float Vertical = Input.GetAxis("Vertical");

		move = direction.right * Horizontal + direction.forward * Vertical;
		characterController.Move(move * speed * Time.deltaTime);

		velocity.y += gravity * Time.deltaTime;
		characterController.Move(velocity * Time.deltaTime);

		//this.rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * 180f * Time.deltaTime, 0);
		//this.transform.Rotate(this.rotation);
		//float angleY = direction.rotation.eulerAngles.y;

		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if (isGrounded && velocity.y < 0) {
			velocity.y = -2f;
		}

		if (Vertical > 0f)
		{
			ChangeSprite(3);
		}
		else if (Vertical < 0f) {
			ChangeSprite(0);
		}

		if (Horizontal > 0f)
		{
			ChangeSprite(4);
			spriteRenderer.flipX = false;
		}
		else if (Horizontal < 0f) {
			spriteRenderer.flipX = true;
			ChangeSprite(4);
		}

		if (Horizontal > 0f && Vertical > 0f)
		{
			spriteRenderer.flipX = false;
			ChangeSprite(2);
		} 
		else if (Horizontal < 0f && Vertical > 0f)
		{
			spriteRenderer.flipX = true;
			ChangeSprite(2);
		}

		if (Horizontal < 0f && Vertical < 0f)
		{
			spriteRenderer.flipX = true;
			ChangeSprite(1);
		} 
		else if (Horizontal > 0f && Vertical < 0f)
		{
			spriteRenderer.flipX = false;
			ChangeSprite(1);
		}

	}
}

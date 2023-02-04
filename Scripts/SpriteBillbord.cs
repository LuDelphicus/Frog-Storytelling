using UnityEngine;

public class SpriteBillbord : MonoBehaviour
{   
    [SerializeField] float headSide = 20f;
    [SerializeField] float rightSide = 30f;
    [SerializeField] float rightFullSide = 60f;
    [SerializeField] float backSide = 100f;
    [SerializeField] float backFullSide = 150f;
    [SerializeField] Transform mainTransform;
    [SerializeField] Transform direction;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    public SpriteRenderer CharacterMain;
    public Sprite[] CharacterArray;

    private Vector3 scaler = new Vector3(0.4f, 0.4f, 0.4f);
    public bool squashed;

    private bool ChangeSprite(int count)
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        
        if (Horizontal != 0 | Vertical != 0) {
            return false;
        }

        CharacterMain.sprite = CharacterArray[count]; 
        return true;
    }

    private void Start()
    {
        CharacterMain = gameObject.GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        //scaler = new Vector3(0.5f, 0.5f, 0.5f);
        transform.localScale = scaler;
        if (squashed == true) {
            scaler.y = Mathf.Lerp(scaler.y, 0.34f, 0.005f); 
            if (scaler.y <= 0.35f) {
                squashed = false;
            }
        } 
        else 
        {
            scaler.y = Mathf.Lerp(scaler.y, 0.4f, 0.005f);
            if (scaler.y >= 0.38f) {
                squashed = true;
            }
        }

        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);

        Vector3 camForwardVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
        float signedAngle = Vector3.SignedAngle(mainTransform.forward, camForwardVector, Vector3.up);
        float angle = Mathf.Abs(signedAngle);

        // Rotation the direction of moving
        direction.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, 0f);

        // Creating the invertion image when angle had -angle
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        //Debug.Log(Horizontal);
        
        if ((Horizontal == 0 && Vertical == 0))
        {
            if (signedAngle < 0 )
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }

        if (angle > rightSide)
        {
            ChangeSprite(2);
        }

        if (angle > rightFullSide)
        {
            ChangeSprite(4);
        }

        if (angle > backSide)
        {
            ChangeSprite(1);
        }

        if (angle > backFullSide)
        {
            ChangeSprite(0);
        }

        if (angle < headSide)
        {
            ChangeSprite(3);
        }
    }

}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float JumpSpeed = 10f;

    private Rigidbody2D body;
    private Animator anim;
    private bool Grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * Speed, body.linearVelocity.y);

        // چرخش پلیر به چپ و راست
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // پرش
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
            Jump();

        // تنظیم پارامترهای انیماتور
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", Grounded);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, JumpSpeed);
        Grounded = false;
        anim.SetTrigger("Jump"); // فرض: یک Trigger به نام Jump در Animator وجود دارد
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            Grounded = true;
    }
}

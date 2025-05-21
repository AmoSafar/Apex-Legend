using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;  // زمان بین دو حمله
    [SerializeField] private Transform arrowPoint;
    [SerializeField] private GameObject[] Arrows;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // اگر کلید Shift چپ زده شد، cooldown تموم شده و پلیر اجازه‌ی حمله داره
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.RightShift) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
        {
            Shoot();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        cooldownTimer = 0f;

    }

    private void Shoot()
    {
            anim.SetTrigger("Shoot");
            cooldownTimer = 0f;

            Arrows[0].transform.position = arrowPoint.position;
            Arrows[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
}
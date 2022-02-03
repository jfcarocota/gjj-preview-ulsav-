using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 3.0f;
    [SerializeField]
    float jumpForce = 7.0f;
    Rigidbody2D rb2D;
    SpriteRenderer spr;
    Animator anim;
    [SerializeField, Range(0.01f, 10f)]
    float rayDistance = 2f;
    [SerializeField]
    Color rayColor = Color.red;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Vector3 rayOrigin;
    [SerializeField]
    Score score;

    GameInputs gameInputs;

    void Awake()
    {
        gameInputs = new GameInputs();
    }

    void OnEnable()
    {
        gameInputs.Enable();
    }

    void OnDisable()
    {
        gameInputs.Disable();
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gameInputs.Gameplay.Jump.performed += _=> Jump();
        gameInputs.Gameplay.Jump.canceled += _=> JumpCanceled();
    }

    //es el update pero para cosas de fisica, eso es porque se ejecuta N veces durante cada frame
    void FixedUpdate()
    {
        rb2D.position += Vector2.right * Axis.x * speed * Time.fixedDeltaTime;
    }

    void Update()
    {
        spr.flipX = FlipSprite;
    }

    void Jump()
    {
        if(IsGrounding)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
    }

    void JumpCanceled()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
    }

    void LateUpdate()
    {
        anim.SetFloat("AxisX", Mathf.Abs(Axis.x));
        anim.SetBool("ground", IsGrounding);
    }

    //depreciated
    Vector2 Axis => new Vector2(gameInputs.Gameplay.AxisX.ReadValue<float>(), gameInputs.Gameplay.AxisY.ReadValue<float>());


    bool FlipSprite => Axis.x > 0 ? false : Axis.x < 0 ? true : spr.flipX;
    bool IsGrounding => Physics2D.Raycast(transform.position + rayOrigin, Vector2.down, rayDistance, groundLayer);

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("coin"))
        {
            Coin coin = col.GetComponent<Coin>();
            score.AddPoints(coin.GetPoints);
            Destroy(col.gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = rayColor;
        Gizmos.DrawRay(transform.position + rayOrigin, Vector2.down * rayDistance);
    }
}

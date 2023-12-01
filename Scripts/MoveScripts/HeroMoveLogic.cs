using System.Collections;
using UnityEngine;

public class HeroMoveLogic : MonoBehaviour
{
    private Rigidbody2D heroRigidBody2D;
    [SerializeField] private float heroMoveSpeed, heroJumpForce;
    [SerializeField] private Animator heroAnimation;
    [SerializeField] private BoxCollider2D heroJumpCollider;
    private float heroMoveHorizontal;
    private bool heroIsJumping = false;

    private void Start()
    {
        heroRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void HeroMove() // move
    {
        heroMoveHorizontal = Input.GetAxisRaw("Horizontal");
        if (heroMoveHorizontal != 0)
        {
            heroAnimation.SetBool("Run", true);
            heroRigidBody2D.velocity = new Vector2(heroMoveHorizontal * heroMoveSpeed, heroRigidBody2D.velocity.y);
        }
        else
        {
            heroRigidBody2D.velocity = new Vector2(0, heroRigidBody2D.velocity.y);
            heroAnimation.SetBool("Run", false);
        }
    }
    private void HeroFlip()
    {
        if (heroMoveHorizontal > 0 && !HeroBloatLogic.isHeroBloated )
        {
            transform.eulerAngles = new Vector2(0, 0f);
        }
        else if (heroMoveHorizontal < 0 && !HeroBloatLogic.isHeroBloated)
        {
            transform.eulerAngles = new Vector2(0, 180f);
        }
    }
    private void HeroJump() // jump
    {
        if (Input.GetButtonDown("Jump") && heroJumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            heroAnimation.SetBool("Jump", true);
            heroRigidBody2D.velocity = new Vector2(heroRigidBody2D.velocity.x, heroJumpForce);
            StartCoroutine(HeroStopJumpDelay());
        }
        else if(heroJumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && heroIsJumping)
        {
            heroAnimation.SetBool("Jump", false);
            heroIsJumping = false;
        }

    }
    private IEnumerator HeroStopJumpDelay()
    {
        yield return new WaitForSeconds(0.1f);
        heroIsJumping = true;
    }

    private void HeroOffJumpAnimation()
    {
        heroAnimation.SetBool("Jump", false);
    }

    private void HeroFall()
    {
        if (heroJumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            heroAnimation.SetBool("Fall", false);
        }
        else heroAnimation.SetBool("Fall", true);
    }

    private void Update()
    {
        HeroMove();
        HeroJump();
        HeroFlip();
        HeroFall();
    }
}

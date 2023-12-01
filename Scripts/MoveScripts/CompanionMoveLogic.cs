using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMoveLogic : MonoBehaviour
{
    private Rigidbody2D companionRigidBody2D;
    [SerializeField] private float companionMoveSpeed, companionJumpForce;
    [SerializeField] private GameObject hero, companion;
    [SerializeField] private Animator companionAnimation;
    [SerializeField] private BoxCollider2D companionJumpCollider;
    private float companionMoveHorizontal;
    private bool isCompanionDoubleJumpAvailable = false;

    private void Start()
    {
        companionRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void CompanionMove()
    {
        companionMoveHorizontal = Input.GetAxisRaw("Horizontal");
        if (companionMoveHorizontal != 0)
        {
            companionAnimation.SetBool("Run", true);
            companionRigidBody2D.velocity = new Vector2(companionMoveHorizontal * companionMoveSpeed, companionRigidBody2D.velocity.y);
        }
        else
        {
            companionRigidBody2D.velocity = new Vector2(0, companionRigidBody2D.velocity.y);
            companionAnimation.SetBool("Run", false);
        }
    }

    private void CompanionFlip()
    {
        if (companionMoveHorizontal > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (companionMoveHorizontal < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void CompanionJump()
    {
        if (Input.GetButtonDown("Jump") && companionJumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "HeroBloatBody")))
        {
            companionAnimation.SetBool("Jump", true);
            companionRigidBody2D.velocity = new Vector2(companionRigidBody2D.velocity.x, companionJumpForce);
            isCompanionDoubleJumpAvailable = true;
        }
        else if(Input.GetButtonDown("Jump") && isCompanionDoubleJumpAvailable)
        {
            companionRigidBody2D.velocity = new Vector2(companionRigidBody2D.velocity.x, companionJumpForce);
            companionAnimation.SetBool("DoubleJump", true);
            companionAnimation.SetBool("Jump", false);
            isCompanionDoubleJumpAvailable = false;
        }
    }

    private void CompanionOffJumpAnimation()
    {
        companionAnimation.SetBool("Jump", false);
        companionAnimation.SetBool("Fall", true);
    }

    private void CompanionOffDoubleJumpAnimation()
    {
        companionAnimation.SetBool("DoubleJump", false);
        companionAnimation.SetBool("Fall", true);
    }

    private void CompanionFall()
    {
        if(companionJumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "HeroBloatBody")))
            companionAnimation.SetBool("Fall", false);
        else
            companionAnimation.SetBool("Fall", true);
    }


    private void Update()
    {
        CompanionMove();
        CompanionJump();
        CompanionFlip();
        CompanionFall();
    }
}

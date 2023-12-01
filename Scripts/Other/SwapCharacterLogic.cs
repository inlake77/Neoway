using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacterLogic : MonoBehaviour
{
    [SerializeField] private Rigidbody2D heroRigidBody2D, companionRigidBody2D;
    [SerializeField] private GameObject hero, companion, heroPoint, companionPoint;
    [SerializeField] private Animator heroAnimation, companionAnimation;
    private bool isHeroCurrentCharacter = true;

    private void SwapCharacterControl()
    {
        if (Input.GetButtonDown("SwapCharacter"))
        {
            if(isHeroCurrentCharacter)
            {
                hero.GetComponent<HeroMoveLogic>().enabled = false;
                hero.GetComponent<HeroBloatLogic>().enabled = false;
                heroAnimation.SetBool("Run", false);
                heroPoint.SetActive(false);
                companionPoint.SetActive(true);
                companion.GetComponent<CompanionMoveLogic>().enabled = true;
                StartCoroutine(SwapCharacterDelay());
            }
            else
            {
                companionPoint.SetActive(false);
                heroPoint.SetActive(true);
                companion.GetComponent<CompanionMoveLogic>().enabled = false;
                companionAnimation.SetBool("Run", false);
                hero.GetComponent<HeroMoveLogic>().enabled = true;
                hero.GetComponent<HeroBloatLogic>().enabled = true;
                StartCoroutine(SwapCharacterDelay());
            }
        }
    }

    private void OffCharacterAnimationsAfterSwap()
    {
        if(!isHeroCurrentCharacter && heroRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            heroAnimation.SetBool("Jump", false);
            heroAnimation.SetBool("Fall", false);
        }
        else if(isHeroCurrentCharacter && companionRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Ground", "Hero")))
        {
            companionAnimation.SetBool("Jump", false);
            companionAnimation.SetBool("Fall", false);
        }
    }

    private IEnumerator SwapCharacterDelay()
    {
        yield return new WaitForSeconds(0.05f);
        if(companion.GetComponent<CompanionMoveLogic>().enabled == true)
            isHeroCurrentCharacter = false;
        else if(hero.GetComponent<HeroMoveLogic>().enabled == true)
            isHeroCurrentCharacter = true;
    }
    void Update()
    {
        SwapCharacterControl();
        OffCharacterAnimationsAfterSwap();
    }
}
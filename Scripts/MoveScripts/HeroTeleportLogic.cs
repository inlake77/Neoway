using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTeleportLogic : MonoBehaviour
{
    private Rigidbody2D heroRigidBody2D;
    [SerializeField] private GameObject hero, companion, companionSpaceError;
    [SerializeField] private Animator heroAnimation, companionAnimation;
    [SerializeField] private BoxCollider2D companionTeleportSpaceChecker;
    private void Start()
    {
        heroRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void HeroTeleportAnimation()
    {
        if (Input.GetButtonDown("Teleport") && !heroAnimation.GetBool("Teleport") && !heroAnimation.GetBool("AfterTeleport")
            && !HeroBloatLogic.isHeroBloated && hero.GetComponent<HeroMoveLogic>().enabled
            && companion.activeInHierarchy && !companionAnimation.GetBool("Fall")
            && !companionTeleportSpaceChecker.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            heroRigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            heroAnimation.SetBool("Teleport", true);
            companionAnimation.SetBool("Teleport", true);
        }
        else if(Input.GetButtonDown("Teleport") && companionTeleportSpaceChecker.IsTouchingLayers(LayerMask.GetMask("Ground"))
            && hero.GetComponent<HeroMoveLogic>().enabled && !HeroBloatLogic.isHeroBloated)
        {
            StartCoroutine(CompanionSpaceErrorBlink());
        }
    } 

    IEnumerator CompanionSpaceErrorBlink()
    {
        companionSpaceError.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        companionSpaceError.SetActive(false);
        StopAllCoroutines();
    }

    private void HeroTeleport()
    {
        heroAnimation.SetBool("Teleport", false);
        hero.transform.position = new Vector2(companion.transform.position.x, companion.transform.position.y + 8f);
        heroAnimation.SetBool("AfterTeleport", true);
    }

    private void HeroAfterTeleport()
    {
        heroAnimation.SetBool("AfterTeleport", false);
        companionAnimation.SetBool("Teleport", false);
        heroRigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        HeroTeleportAnimation();
    }
}
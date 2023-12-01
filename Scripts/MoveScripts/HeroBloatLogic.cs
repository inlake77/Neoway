using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroBloatLogic : MonoBehaviour
{
    private Rigidbody2D heroRigidBody2D;
    [SerializeField] private Animator heroAnimation;
    [SerializeField] private GameObject heroPoint, heroSpaceError;
    [SerializeField] private BoxCollider2D heroBloatSpaceChecker;
    public static bool isHeroBloated;
    private void Start()
    {
        heroRigidBody2D = GetComponent<Rigidbody2D>();
        isHeroBloated = false;
    }

    private void HeroBloat()
    {
        if (Input.GetButtonDown("Bloat"))
        {
            if (!isHeroBloated && !heroAnimation.GetBool("Teleport") && !heroAnimation.GetBool("AfterTeleport")
                && !heroBloatSpaceChecker.IsTouchingLayers(LayerMask.GetMask("CompanionBody")))
            {
                heroRigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                heroAnimation.SetBool("Bloat", true);
                heroAnimation.SetBool("Deflate", false);
                heroAnimation.SetBool("Jump", false);
                heroPoint.transform.position = new Vector2(transform.position.x, transform.position.y + 5f);
                isHeroBloated = true;
            }
            else if (isHeroBloated && !heroAnimation.GetBool("Teleport") && !heroAnimation.GetBool("AfterTeleport"))
            {
                heroRigidBody2D.constraints = RigidbodyConstraints2D.None;
                heroRigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                heroAnimation.SetBool("Bloat", false);
                heroPoint.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
                isHeroBloated = false;
            }
            else if(heroBloatSpaceChecker.IsTouchingLayers(LayerMask.GetMask("CompanionBody")))
            {
                StartCoroutine(HeroSpaceErrorBlink());
            }
        }
    }

    IEnumerator HeroSpaceErrorBlink()
    {
        heroSpaceError.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        heroSpaceError.SetActive(false);
        StopAllCoroutines();
    }

    public void HeroDeflate()
    {
        heroAnimation.SetBool("Deflate", true);
    }

    void Update()
    {
        HeroBloat();
    }
}

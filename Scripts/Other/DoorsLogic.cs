using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsLogic : MonoBehaviour
{
    [SerializeField] private GameObject doorButton;
    [SerializeField] private Animator doorAnimation;

    private void OpenDoor()
    {
        if(doorButton.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("HeroBody", "CompanionBody")))
        {
            doorAnimation.SetBool("Open", true);
            doorButton.SetActive(false);
        }
    }

    void Update()
    {
        OpenDoor();
    }
}

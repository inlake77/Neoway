using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class SwapLevelLogic : MonoBehaviour
{
    [SerializeField] private GameObject hero, companion, fadeOutNextLevel, fadeOutRestart,
        charactersFinishPlatformBarriers, heroFinishPlatformBarriers, companionFinishPlatformBarriers;
    [SerializeField] private Rigidbody2D heroRigidbody2D, companionRigidbody2D;
    public static bool levelComplete, characterDeath;

    private void Start()
    {
        levelComplete = false;
        characterDeath = false;
    }

    void RestartLevel()
    {
        if (Input.GetButtonDown("RestartLevel") && !fadeOutNextLevel.activeInHierarchy)
            fadeOutRestart.SetActive(true);
    }
    void CharactersDeath()
    {
        if(hero.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("SpikesCollider"))
            || companion.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("SpikesCollider")))
        {
            characterDeath = true;
            heroRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            companionRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            hero.GetComponent<HeroMoveLogic>().enabled = false;
            hero.GetComponent<HeroBloatLogic>().enabled = false;
            companion.GetComponent<CompanionMoveLogic>().enabled = false;
            fadeOutRestart.SetActive(true);
        }
    }
    void StartLoadNextAnimationLevel()
    {
        if (hero.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("CharactersFinishPlatformNextLevelTrigger"))
            && companion.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("CharactersFinishPlatformNextLevelTrigger")))
        {
            charactersFinishPlatformBarriers.SetActive(true);
            levelComplete = true;
            fadeOutNextLevel.SetActive(true);
        }
        else if(hero.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("HeroFinishPlatformNextLevelTrigger"))
            && companion.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("CompanionFinishPlatformNextLevelTrigger")))
        {
            heroFinishPlatformBarriers.SetActive(true);
            companionFinishPlatformBarriers.SetActive(true);
            levelComplete = true;
            fadeOutNextLevel.SetActive(true);
        }
    }

    void Update()
    {
        RestartLevel();
        CharactersDeath();
        StartLoadNextAnimationLevel();
    }
}

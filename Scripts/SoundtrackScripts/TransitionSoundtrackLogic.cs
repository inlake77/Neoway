using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionSoundtrackLogic : MonoBehaviour
{
    [SerializeField] Animator transitionSoundtrackAnimation;
    [SerializeField] GameObject transitionSoundtrackOriginal;
    [SerializeField] private string transitionSceneName;
    private static bool transitionSoundtrackIsPlaying = false;


    private void Awake()
    {
        if (transitionSoundtrackIsPlaying)
            transitionSoundtrackOriginal.SetActive(false);
    }

    private void Start()
    {
        if (!transitionSoundtrackIsPlaying)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
            transitionSoundtrackIsPlaying = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != transitionSceneName)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            transitionSoundtrackAnimation.SetBool("OffVolume", true);
        }
    }

    private void DestroyTransitionSoundtrack()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WindSoundtrackLogic : MonoBehaviour
{
    [SerializeField] Animator windSoundtrackAnimation;
    [SerializeField] GameObject windSoundtrackOriginal;
    private static bool windSoundtrackIsPlaying = false;

    private void Awake()
    {
        if(windSoundtrackIsPlaying)
            windSoundtrackOriginal.SetActive(false);
    }

    private void Start()
    {
        if (!windSoundtrackIsPlaying)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
            windSoundtrackIsPlaying = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartMenu")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            windSoundtrackAnimation.SetBool("OffVolume", true);
        }
    }

    private void DestroyWindSoundtrack()
    {
        Destroy(gameObject);
    }
}

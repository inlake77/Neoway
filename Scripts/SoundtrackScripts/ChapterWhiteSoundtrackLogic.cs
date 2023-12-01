using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterWhiteSoundtrackLogic : MonoBehaviour
{
    [SerializeField] Animator chapterWhiteSoundtrackAnimation;
    [SerializeField] GameObject chapterWhiteSoundtrackOriginal;
    private static bool chapterWhiteSoundtrackIsPlaying = false;

    private void Awake()
    {
        if(chapterWhiteSoundtrackIsPlaying)
            chapterWhiteSoundtrackOriginal.SetActive(false);
    }

    private void Start()
    {
        if (!chapterWhiteSoundtrackIsPlaying)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
            chapterWhiteSoundtrackIsPlaying = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "White To Blue Transition")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            chapterWhiteSoundtrackAnimation.SetBool("OffVolume", true);
        }
    }

    private void DestroyChapterWhiteSoundtrack()
    {
        Destroy(gameObject);
    }
}
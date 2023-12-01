using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterGreenSoundtrackLogic : MonoBehaviour
{
    [SerializeField] Animator chapterGreenSoundtrackAnimation;
    [SerializeField] GameObject chapterGreenSoundtrackOriginal;
    private static bool chapterGreenSoundtrackIsPlaying = false;

    private void Awake()
    {
        if (chapterGreenSoundtrackIsPlaying)
            chapterGreenSoundtrackOriginal.SetActive(false);
    }

    private void Start()
    {
        if (!chapterGreenSoundtrackIsPlaying)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
            chapterGreenSoundtrackIsPlaying = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Green To Yellow Transition")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            chapterGreenSoundtrackAnimation.SetBool("OffVolume", true);
        }
    }

    private void DestroyChapterGreenSoundtrack()
    {
        Destroy(gameObject);
    }
}

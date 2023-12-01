using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterYellowSoundtrackLogic : MonoBehaviour
{
    [SerializeField] Animator chapterYellowSoundtrackAnimation;
    [SerializeField] GameObject chapterYellowSoundtrackOriginal;
    private static bool chapterYellowSoundtrackIsPlaying = false;

    private void Awake()
    {
        if (chapterYellowSoundtrackIsPlaying)
            chapterYellowSoundtrackOriginal.SetActive(false);
    }

    private void Start()
    {
        if (!chapterYellowSoundtrackIsPlaying)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
            chapterYellowSoundtrackIsPlaying = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Yellow To Red Transition")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            chapterYellowSoundtrackAnimation.SetBool("OffVolume", true);
        }
    }

    private void DestroyChapterYellowSoundtrack()
    {
        Destroy(gameObject);
    }
}

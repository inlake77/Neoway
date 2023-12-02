using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterRedSoundtrackLogic : MonoBehaviour
{
    [SerializeField] Animator chapterRedSoundtrackAnimation;
    [SerializeField] GameObject chapterRedSoundtrackOriginal;
    private static bool chapterRedSoundtrackIsPlaying = false;

    private void Awake()
    {
        if (chapterRedSoundtrackIsPlaying)
            chapterRedSoundtrackOriginal.SetActive(false);
    }

    private void Start()
    {
        if (!chapterRedSoundtrackIsPlaying)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
            chapterRedSoundtrackIsPlaying = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Red To Multicolored Transition")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            chapterRedSoundtrackAnimation.SetBool("OffVolume", true);
        }
    }

    private void DestroyChapterRedSoundtrack()
    {
        chapterRedSoundtrackIsPlaying = false;
        Destroy(gameObject);
    }
}

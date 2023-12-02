using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterBlueSoundtrackLogic : MonoBehaviour
{
    [SerializeField] Animator chapterBlueSoundtrackAnimation;
    [SerializeField] GameObject chapterBlueSoundtrackOriginal;
    private static bool chapterBlueSoundtrackIsPlaying = false;

    private void Awake()
    {
        if (chapterBlueSoundtrackIsPlaying)
            chapterBlueSoundtrackOriginal.SetActive(false);
    }

    private void Start()
    {
        if (!chapterBlueSoundtrackIsPlaying)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
            chapterBlueSoundtrackIsPlaying = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Blue To Green Transition")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            chapterBlueSoundtrackAnimation.SetBool("OffVolume", true);
        }
    }

    private void DestroyChapterBlueSoundtrack()
    {
        chapterBlueSoundtrackIsPlaying = false;
        Destroy(gameObject);
    }
}

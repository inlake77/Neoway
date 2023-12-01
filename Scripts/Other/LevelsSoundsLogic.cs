using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsSoundsLogic : MonoBehaviour
{
    [SerializeField] GameObject soundsOriginal;
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip levelCompleteClip, characterDeathClip;
    private string tempSceneName = "Level 1";
    private static bool soundsIsActive = false;
    private bool levelCompleteAudioIsPlayed, characterDeathAudioIsPlayed;

    private void Awake()
    {
        if (soundsIsActive)
            soundsOriginal.SetActive(false);
    }
    private void Start()
    {
        if(!soundsIsActive)
        {
            SceneManager.sceneLoaded += OnCurrentSceneLoaded;
            DontDestroyOnLoad(gameObject);
            soundsIsActive = true;
        }
    }
    private void OnCurrentSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != tempSceneName)
        {
            tempSceneName = scene.name;
            levelCompleteAudioIsPlayed = false;
        }
        if(characterDeathAudioIsPlayed && scene.name == tempSceneName)
            characterDeathAudioIsPlayed = false;
    }
    private void LevelCompleteSound()
    {
        if (SwapLevelLogic.levelComplete && !levelCompleteAudioIsPlayed)
        {
            soundSource.PlayOneShot(levelCompleteClip, 1f);
            levelCompleteAudioIsPlayed = true;
        }
    }
    private void CharacterDeathSound()
    {
        if (SwapLevelLogic.characterDeath && !characterDeathAudioIsPlayed)
        {
            soundSource.PlayOneShot(characterDeathClip, 0.5f);
            characterDeathAudioIsPlayed = true;
        }
    }
    private void Update()
    {
        LevelCompleteSound();
        CharacterDeathSound();
    }
}
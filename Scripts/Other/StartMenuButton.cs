using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenuButton : MonoBehaviour
{
    [SerializeField] GameObject fadeOut;
    void Button()
    {
        if(Input.anyKey)
            fadeOut.SetActive(true);
    }
    void Update()
    {
        Button();
    }
}

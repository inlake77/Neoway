using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
    [SerializeField] GameObject fadeOut;
    void Button()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            fadeOut.SetActive(true);
    }
    void Update()
    {
        Button();
    }
}

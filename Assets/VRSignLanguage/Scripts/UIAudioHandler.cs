using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private Button targetButton;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("UIAudio").GetComponent<AudioSource>();
        targetButton.onClick.AddListener(() => audioSource.Play());
    }
}

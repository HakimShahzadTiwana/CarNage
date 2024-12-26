using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBehaviour : MonoBehaviour
{
    private AudioSource policeSiren;
    public GameObject redLight;
    public GameObject blueLight;
    void Start()
    {
        policeSiren = GetComponent<AudioSource>();
        InvokeRepeating(nameof(RedLight), 0, 1);
        InvokeRepeating(nameof(BlueLight), 0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControl.isMuted)
        {
            policeSiren.mute = true;
        }
        else {
            policeSiren.mute = false;
        }
    }

    void RedLight() {
        redLight.SetActive(true);
        blueLight.SetActive(false);
    }
    void BlueLight() {
        redLight.SetActive(false);
        blueLight.SetActive(true);
    }

}

using System;
using UnityEngine;

public class BootLoader : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        
        gameManager.Init();
    }
}

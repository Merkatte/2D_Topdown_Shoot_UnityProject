using System;
using UnityEngine;

public class BootLoader : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager.Init();
    }
}

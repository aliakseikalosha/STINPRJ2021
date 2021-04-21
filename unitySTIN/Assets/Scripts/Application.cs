using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour
{
    [SerializeField] private UserInterface ui;

    private void Awake()
    {
        ui.DiffrenceWindow.Show();
    }
}

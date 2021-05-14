using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour
{
    [SerializeField] private UserInterface ui;
    [SerializeField] private DataManager data;
    private void Awake()
    {
        ui.DiffrenceWindow.Show();
    }
}

using System.Collections;
using UnityEngine;

public class Application : MonoBehaviour
{
    [SerializeField] private UserInterface ui;
    [SerializeField] private DataManager data;

    private IEnumerator Start()
    {
        yield return new WaitUntil(()=>DataManager.I.HasDataForDay(DataManager.I.CurrentDay));
        ui.DiffrenceWindow.Show();
    }
}

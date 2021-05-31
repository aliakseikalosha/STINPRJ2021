using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;

namespace Assets.Scripts
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private UserInterface ui;
        [SerializeField] private DataManager data;

        private void Awake()
        {
            UnityEngine.Application.targetFrameRate = 60;
            Analytics.initializeOnStartup = true;
            Analytics.enabled = true;
            Analytics.ResumeInitialization();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => DataManager.I.HasDataForDay(DataManager.I.CurrentDay));
            ui.DiffrenceWindow.Show();
        }
    }
}
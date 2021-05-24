using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public static UserInterface I { get; private set; }
    public DiffrenceWindow DiffrenceWindow => GetWindow<DiffrenceWindow>();
    public VaccinationWindow VaccinationWindow => GetWindow<VaccinationWindow>();

    [SerializeField] private Transform windowParrent = null;
    [SerializeField] private List<Window> prefabs = new List<Window>();
    private List<Window> windows = new List<Window>();

    private T GetWindow<T>() where T : Window
    {
        var window = windows.FirstOrDefault(c => c is T);
        if (window == null)
        {
            window = Instantiate(prefabs.First(c => c is T), windowParrent);
            windows.Add(window);
        }
        return window as T;
    }

    private void Awake()
    {
        I = this;
    }

    public abstract class Window : MonoBehaviour
    {
        public Action OnShown;
        public Action OnHiden;

        [SerializeField] protected Canvas canvas = null;
        [SerializeField] protected Button nextDay = null;
        [SerializeField] protected TMP_Text nextDayText = null;
        [SerializeField] protected Button prevDay = null;
        [SerializeField] protected TMP_Text prevDayText = null;
        [SerializeField] protected Button refresh = null;
        [SerializeField] protected Button otherScreen = null;
        protected DateTime currentDay;
        protected Coroutine refreshing = null;

        protected virtual void Awake()
        {
            canvas.gameObject.SetActive(false);
            nextDay.onClick.AddListener(GoToNextDay);
            prevDay.onClick.AddListener(GoToPrevDay);
            otherScreen.onClick.AddListener(GoToOtherScreen);
            refresh.onClick.AddListener(RefreshData);
            DataManager.I.OnNewData += OnGetNewData;
        }

        protected abstract void GoToOtherScreen();
        protected void RefreshData()
        {
            if (refreshing == null)
            {
                refreshing = StartCoroutine(StartRefresh());
            }
        }
        protected virtual IEnumerator StartRefresh()
        {
            var wating = true;
            DataManager.I.TryGetNewData((b) => wating = false);
            refresh.interactable = false;
            while (wating)
            {
                yield return null;
            }
            refresh.interactable = true;
            ShowDay(DataManager.I.CurrentDay);
            refreshing = null;
        }
        protected virtual void GoToPrevDay()
        {
            if (DataManager.I.HasDataForPrevDay(currentDay))
            {
                ShowDay(DataManager.I.PrevDayWithData(currentDay));
            }
        }
        protected virtual void GoToNextDay()
        {
            if (DataManager.I.HasDataForNextDay(currentDay))
            {
                ShowDay(DataManager.I.NextDayWithData(currentDay));
            }
        }
        protected virtual void OnGetNewData()
        {
            ShowDay(currentDay);
        }

        protected virtual void ShowDay(DateTime day)
        {
            currentDay = day;
            nextDay.interactable = DataManager.I.HasDataForNextDay(currentDay);
            nextDayText.text = nextDay.interactable ? $"{DataManager.I.NextDayWithData(currentDay):dd/MM/yyyy}" : "No Data";
            prevDay.interactable = DataManager.I.HasDataForPrevDay(currentDay);
            prevDayText.text = prevDay.interactable ? $"{DataManager.I.PrevDayWithData(currentDay):dd/MM/yyyy}" : "No Data";
            refresh.interactable = (day.Date == DataManager.I.CurrentDay.Date);
        }

        public virtual void Show()
        {
            OnShown?.Invoke();
            canvas.gameObject.SetActive(true);
            Init();
        }

        protected virtual void Init()
        {
            ShowDay(DataManager.I.CurrentDay);
        }

        public virtual void Hide()
        {
            OnHiden?.Invoke();
            if (refreshing != null)
            {
                StopCoroutine(refreshing);
                refreshing = null;
                refresh.interactable = true;
            }
            canvas.gameObject.SetActive(false);
        }
    }
}

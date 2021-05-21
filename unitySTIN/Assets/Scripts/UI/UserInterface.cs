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
        protected DateTime curretDay;
        protected Coroutine refreshing = null;

        protected virtual void Awake()
        {
            canvas.gameObject.SetActive(false);
            nextDay.onClick.AddListener(GoToNextDay);
            prevDay.onClick.AddListener(GoToPrevDay);
            otherScreen.onClick.AddListener(GoToOtherScreen);
            refresh.onClick.AddListener(RefreshData);
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
            DataManager.I.TryGetNewData(() => wating = false);
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
            if (DataManager.I.HasPrevDayData)
            {
                ShowDay(DataManager.I.PrevDayData);
            }
        }
        protected virtual void GoToNextDay()
        {
            if (DataManager.I.HasNextDayData)
            {
                ShowDay(DataManager.I.NextDayData);
            }
        }

        protected virtual void ShowDay(DateTime day)
        {
            nextDay.interactable = DataManager.I.HasNextDayData;
            nextDayText.text = nextDay.interactable ? $"{DataManager.I.NextDay:dd/MM/yyyy}" : "No Data";
            prevDay.interactable = DataManager.I.HasPrevDayData;
            prevDayText.text = prevDay.interactable ? $"{DataManager.I.PrevDay:dd/MM/yyyy}" : "No Data";
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

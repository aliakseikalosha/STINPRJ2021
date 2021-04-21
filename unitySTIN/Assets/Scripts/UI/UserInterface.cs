using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public DiffrenceWindow DiffrenceWindow => GetWindow<DiffrenceWindow>();

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
    public abstract class Window : MonoBehaviour
    {
        public Action OnShown;
        public Action OnHiden;

        [SerializeField] protected Canvas canvas = null;

        protected virtual void Awake()
        {
            canvas.gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            OnShown?.Invoke();
            canvas.gameObject.SetActive(true);
            Init();
        }

        protected abstract void Init();

        public virtual void Hide()
        {
            OnHiden?.Invoke();
            canvas.gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager inst = null;
    public static NetworkManager I => inst;

    private void Awake()
    {
        if (I != null)
        {
            Debug.LogError($"THERE ARE 2 {this.GetType().Name}");
        }
        inst = this;
    }
    public void Get(string url, Action<string> onSuccess, Action<string> onError)
    {
        StartCoroutine(GetRequest(url, (w) =>
        {
            switch (w.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError($": HTTP Error: {w.error}");
                    onError?.Invoke(w.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log($":Received: {w.downloadHandler.text}");
                    onSuccess?.Invoke(w.downloadHandler.text);
                    break;
            }
        }));
    }

    public void Get(string url, Action<UnityWebRequest> onDownload)
    {
        StartCoroutine(GetRequest(url, onDownload));
    }

    IEnumerator GetRequest(string uri, Action<UnityWebRequest> onDownload)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            onDownload?.Invoke(webRequest);
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}

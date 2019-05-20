using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadingScreen : MonoBehaviour
{
    public Text loadingText;
    private static UILoadingScreen instance;
   
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        instance.SetVisible(false);
    }

    public static UILoadingScreen Instance
    {
        get { return instance; }
    }

    public void SetVisible(bool show)
    {
        gameObject.SetActive(show);
    }

    public void Update()
    {
        int loadingVal = (int)(LoaderManager.Instance.loadingProgress * 100);
        loadingText.text = loadingVal + "%";
        if (LoaderManager.Instance.loadingProgress >= 1)
            SetVisible(false);
    }
}

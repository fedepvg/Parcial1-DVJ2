using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField]
    private Button Button;

    void Awake()
    {
        if (Button == null)
            Button = GetComponent<Button>();

        Button.onClick.AddListener(OnClick);
        UILoadingScreen.Instance.SetVisible(false);
    }

    private void OnClick()
    {
        if(GameManager.Instance)
        {
            GameManager.Instance.Initialize();
        }
        LoaderManager.Instance.LoadScene("Level1");
        UILoadingScreen.Instance.SetVisible(true);
    }
}

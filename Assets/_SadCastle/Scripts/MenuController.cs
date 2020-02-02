using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    public UnityEventBool OnMenuVisibilityChanged;
    public UnityEventGameObject OnMenuStateChanged;

    public bool isMenuVisible;

    public float startDelaySeconds = 0.0f;
    public bool showOnStart = false;

    public GameObject[] panels;
    public GameObject defaultActivePanel;

    public bool canHide;

    public void SetActivePanel(GameObject newActivePanel)
    {
        foreach(var pan in panels)
        {
            pan.SetActive(false);
        }
        newActivePanel.SetActive(true);

        OnMenuStateChanged?.Invoke(newActivePanel);
    }

    [System.Serializable]
    public class ElementStartStop
    {
        public RectTransform elementRect;
        public Vector2 startPosition;
        public Vector2 stopPosition;
        public float showDurationMs;
        public float hideDurationMs;

    }
    public ElementStartStop[] elements;

    private void Start()
    {
        if (showOnStart)
        {
            ShowMenu(startDelaySeconds);
        }

        SetActivePanel(defaultActivePanel);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (isMenuVisible)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }

    [ContextMenu("Show Menu")]
    public void ShowMenu(float delay = 0.0f)
    {
        OnMenuVisibilityChanged?.Invoke(true);

        foreach (var elem in elements)
        {
            elem.elementRect.DOKill();
            elem.elementRect.DOAnchorPos(elem.stopPosition, elem.showDurationMs / 1000.0f, false).SetDelay(delay);
        }

        isMenuVisible = true;
    }

    [ContextMenu("Hide Menu")]
    public void HideMenu()
    {
        if(!canHide) { return; }

        OnMenuVisibilityChanged?.Invoke(false);

        foreach (var elem in elements)
        {
            //elem.elementRect.DORewind(false);
            elem.elementRect.DOKill();
            elem.elementRect.DOAnchorPos(elem.startPosition, elem.hideDurationMs / 1000.0f, false).SetEase(Ease.InBack);
        }

        isMenuVisible = false;
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OnValidate()
    {

    }
}

[System.Serializable]
public class UnityEventGameObject : UnityEvent<GameObject> { }
[System.Serializable]
public class UnityEventBool : UnityEvent<bool> { }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    public UnityEventBool OnMenuVisibilityChanged;
    public UnityEventGameObject OnMenuStateChanged;

    [SerializeField]
    private Camera menuCamera;
    [SerializeField]
    private float cameraZoomDelay = 0.2f;

    public bool isMenuVisible;

    public float startDelaySeconds = 0.0f;
    public bool showOnStart = false;

    public GameObject[] panels;
    public GameObject defaultActivePanel;

    [SerializeField]
    private Image screenSpaceColorQuad;

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
    public void HideMenu(bool force=false)
    {
        if(!canHide && !force) { return; }

        OnMenuVisibilityChanged?.Invoke(false);

        foreach (var elem in elements)
        {
            //elem.elementRect.DORewind(false);
            elem.elementRect.DOKill();
            elem.elementRect.DOAnchorPos(elem.startPosition, elem.hideDurationMs / 1000.0f, false).SetEase(Ease.InBack);
        }

        isMenuVisible = false;
    }

    public float transitionToGameDuration = 1.0f;

    public void PlayGame()
    {
        canHide = true;
        HideMenu(true);

        screenSpaceColorQuad.DOColor(Color.white, transitionToGameDuration).SetDelay(cameraZoomDelay).SetEase(Ease.InCubic);
        menuCamera.DOFieldOfView(20, transitionToGameDuration).SetDelay(cameraZoomDelay);
        //menuCamera.transform.DOLookAt(menuCamera.transform.position + Vector3.right * 20.968f, 0.5f);
        menuCamera.transform.DORotate(Vector3.right * 20.968f, transitionToGameDuration);

        DOVirtual.DelayedCall(cameraZoomDelay + 1.0f, () => Debug.Log("gogo"));
    }

    public void ExitGame()
    {
        canHide = true;
        HideMenu(true);

        screenSpaceColorQuad.DOColor(Color.black, transitionToGameDuration).SetDelay(cameraZoomDelay).SetEase(Ease.InCubic);
        menuCamera.DOFieldOfView(20, transitionToGameDuration).SetDelay(cameraZoomDelay);
        //menuCamera.transform.DOLookAt(menuCamera.transform.position + Vector3.right * 20.968f, 0.5f);
        menuCamera.transform.DORotate(Vector3.right * -20.968f, transitionToGameDuration);

        DOVirtual.DelayedCall(cameraZoomDelay + 1.0f, () => ActuallyExitGame());
    }

    private void ActuallyExitGame()
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

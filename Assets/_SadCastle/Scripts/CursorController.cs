using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Camera cursorCamera;

    private RaycastHit[] pickerHits;
    private int pickerHitCount = 0;
    [SerializeField]
    private int maxPickerHits = 32;
    [SerializeField]
    private LayerMask pickerMask;

    [SerializeField]
    private CursorState[] cursorStates;
    [SerializeField]
    private CursorState fallbackCursorState;

    private void Awake()
    {
        pickerHits = new RaycastHit[maxPickerHits];
    }

    private void Update()
    {
        if(cursorCamera == null) { cursorCamera = Camera.main; Debug.LogWarning("Cursor Camera was not assigned. Defaulting to main camera..."); }

        var pickerRay = cursorCamera.ScreenPointToRay(Input.mousePosition);
        pickerHitCount = Physics.RaycastNonAlloc(pickerRay, pickerHits, Mathf.Infinity, pickerMask, QueryTriggerInteraction.Ignore);

        if (pickerHitCount == 0)
        {
            SetCursor(fallbackCursorState);
        }
        else
        {
            bool matchFound = false;

            foreach (var candidateState in cursorStates)
            {
                var cursorRule = candidateState;
                for (int i = 0; i < pickerHitCount; ++i)
                {
                    if (cursorRule.MatchTarget(pickerHits[i].collider.gameObject))
                    {
                        SetCursor(cursorRule);
                        matchFound = true;
                        break;
                    }
                }
            }

            if(!matchFound)
            {
                SetCursor(fallbackCursorState);
            }
        }
    }

    private void SetCursor(CursorState cState)
    {
        Cursor.SetCursor(cState.cursorTexture, cState.cursorHotspot, CursorMode.Auto);
    }

    private void Reset()
    {
        cursorCamera = Camera.main;
    }
}

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
        var pickerRay = cursorCamera.ScreenPointToRay(Input.mousePosition);
        pickerHitCount = Physics.RaycastNonAlloc(pickerRay, pickerHits, Mathf.Infinity, pickerMask, QueryTriggerInteraction.Ignore);

        if (pickerHitCount == 0)
        {
            SetCursor(fallbackCursorState);
        }
        else
        {
            foreach (var candidateState in cursorStates)
            {
                var cursorRule = candidateState;
                for (int i = 0; i < pickerHitCount; ++i)
                {
                    if (cursorRule.MatchTarget(pickerHits[i].collider.gameObject))
                    {
                        SetCursor(cursorRule);
                    }
                }
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

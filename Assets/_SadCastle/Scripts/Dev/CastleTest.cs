using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CastleTest : MonoBehaviour 
{
    [SerializeField] bool doDestructionTest = false;
    bool destructionTestDone = false;
    [SerializeField] Vector2 destructionTestNums;

    Castle castle;

    void Awake()
    {
       castle = GetComponent<Castle>(); 
    }
    void Update()
    {
        if (doDestructionTest && !destructionTestDone)
        {
            castle.DestroyChunk((int) destructionTestNums.x, (int) destructionTestNums.y);
            destructionTestDone = true;
        }
    }
}
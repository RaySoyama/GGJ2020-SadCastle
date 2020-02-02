using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DestroyedChunkMatcher : CursorStateMatcher
{
    protected override bool MatchByCustom(GameObject target)
    {
        var chunk = target.GetComponent<CastleChunk>();
        if (chunk == null) { return false; }

        return chunk.CanRepair();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] CastleChunkRow[] castleChunkRows;

    CastleChunk GetChunk(int row, int column) 
    {
        return castleChunkRows[row].CastleChunks[column];
    }
    
    public void DestroyChunk(int row, int column) 
    {
        CastleChunk chunk = GetChunk(row, column);
        chunk.Destroy();
    }
}

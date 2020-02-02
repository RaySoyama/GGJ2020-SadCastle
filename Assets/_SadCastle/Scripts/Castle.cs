using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] CastleChunkRow[] castleChunkRows;

    public int totalCastleChunks
    {
        get
        {
            int total = 0;
            foreach(var row in castleChunkRows)
            {
                total += row.CastleChunks.Length;
            }

            return total;
        }
    }
    public int totalHealthyCastleChunks
    {
        get
        {
            int total = 0;
            foreach (var row in castleChunkRows)
            {
                foreach (var chunk in row.CastleChunks)
                {
                    if (!chunk.CanRepair()) { ++total; }
                }
            }

            return total;
        }
    }

    public CastleChunk GetChunk(int row, int column) 
    {
        return castleChunkRows[row].CastleChunks[column];
    }
    
    public void DestroyChunk(int row, int column) 
    {
        CastleChunk chunk = GetChunk(row, column);
        chunk.Destroy();
    }
}

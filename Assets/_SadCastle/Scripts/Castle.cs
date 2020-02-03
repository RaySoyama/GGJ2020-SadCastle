using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] CastleChunkRow[] castleChunkRows;

    void Awake() 
    {
        InitializeChunkRowAndColumnNumbers();
    }

    void InitializeChunkRowAndColumnNumbers() 
    {
        for (int i = 0; i < castleChunkRows.Length; i++)
        {
            CastleChunkRow row = castleChunkRows[i];
            for (int j = 0; j < row.CastleChunks.Length; j++) 
            {
                CastleChunk chunk = row.CastleChunks[j];

                chunk.SetRowAndColumnNumbers(i, j);
            }
        }
    }

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

    public void UpdateMeshes()
    {
        foreach (CastleChunkRow row in castleChunkRows)
        {
            foreach (CastleChunk chunk in row.CastleChunks) {
                chunk.UpdateMesh();
            }
        }
    }
}

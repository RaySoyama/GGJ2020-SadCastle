using UnityEngine;

[System.Serializable]
public class CastleChunkRow 
{
    [SerializeField] CastleChunk[] castleChunks;

    public CastleChunk[] CastleChunks 
    {
        get 
        {
            return castleChunks;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum GrassState {Normal,Burning,Burend}

public class GrassFIreManager : MonoBehaviour
{

    [SerializeField] private Tilemap grassTilemap;
    [SerializeField] private TileBase burningTile;  // 불타는 그래픽
    [SerializeField] private TileBase burnedTile;   // 다 탄 그래픽

    private Dictionary<Vector3Int, GrassState> grassState = new Dictionary<Vector3Int, GrassState>();

    void Start()
    {
        foreach (Vector3Int pos in grassTilemap.cellBounds.allPositionsWithin)
        {
            if (grassTilemap.HasTile(pos))
            {
                grassState[pos] = GrassState.Normal;
            }
        }

        Ignite(new Vector3Int(6, 0, 0));
    }

    private void Ignite(Vector3Int worldPos)
    {
        Vector3Int cell = grassTilemap.WorldToCell(worldPos);
        IgniteCell(cell);
    }

    private void IgniteCell(Vector3Int cell)
    {
        if (!grassState.ContainsKey(cell)) return;
        if (grassState[cell] != GrassState.Normal) return;

        grassState[cell] = GrassState.Burning;
        grassTilemap.SetTile(cell, burningTile);

        StartCoroutine(SpreadFire(cell));
    }

    private IEnumerator SpreadFire(Vector3Int startCell)
    {
        yield return new WaitForSeconds(1f);

        grassState[startCell] = GrassState.Burend;
        grassTilemap.SetTile(startCell, burnedTile);

        Vector3Int[] dirs = new Vector3Int[]
        {
            new Vector3Int(1,0,0),
            new Vector3Int(-1,0,0),
            new Vector3Int(0,1,0),
            new Vector3Int(0,-1,0)
        };

        foreach (Vector3Int dir in dirs)
        {
            Vector3Int next = startCell + dir;

            if (grassState.ContainsKey(next) && grassState[next] == GrassState.Normal)
            {
                IgniteCell(next);
            }
        }
    }

}

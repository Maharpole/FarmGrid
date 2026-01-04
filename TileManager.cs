using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TiileManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Tilemap targetTilemap;     // your main tilemap
    [SerializeField] private Tilemap highlightTilemap;  // overlay tilemap
    [SerializeField] private TileBase highlightTile;    // translucent tile

    private Vector3Int lastCell = new Vector3Int(int.MinValue, int.MinValue, 0);

    void Reset()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (!cam || !targetTilemap || !highlightTile || highlightTile == null)
            return;

        Vector3 world = cam.ScreenToWorldPoint(Input.mousePosition);
        world.z = 0f;

        Vector3Int cell = targetTilemap.WorldToCell(world);

        if (cell == lastCell) return;

        if (lastCell.x != int.MinValue)
            highlightTilemap.SetTile(lastCell, null);

        highlightTilemap.SetTile(cell, highlightTile);
        lastCell = cell;
    }
}
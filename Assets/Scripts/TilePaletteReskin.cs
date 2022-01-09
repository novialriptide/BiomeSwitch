using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePaletteReskin : MonoBehaviour
{
    Tilemap tm;

    private void Start()
    {
        tm = GetComponentInChildren<Tilemap>();
    }

    public void Reskin(TileBase[] palette)
    {
        for (int x = (int) tm.localBounds.min.x; x < tm.localBounds.max.x; x++)
        {
            for (int y = (int)tm.localBounds.min.y; y < tm.localBounds.max.y; y++)
            {
                TileBase tb = tm.GetTile(new Vector3Int(x, y, 0));
                if (tb != null)
                {
                    foreach (TileBase t in palette)
                    {
                        string[] nameTB = tb.name.Split('_');
                        string[] nameT = t.name.Split('_');

                        if (nameTB[nameTB.Length - 1] == nameT[nameT.Length - 1])
                        {
                            tm.SwapTile(tb, t);
                        }
                    }
                }
            }
        }
    }
}

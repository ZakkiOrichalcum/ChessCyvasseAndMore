using UnityEngine;
using System.Collections;

public class HexBoard : MonoBehaviour, IBoard
{
    public HexTile HexTile;

    public float CellSize { get; set; }
    public int Radius { get; set; }
    public int Diameter { get; set; }
    public BoardType HexBoardType { get; set; }

    private HexTile[,] Tiles { get; set; }

    public void Create(BoardType boardType, int size, float cellsize)
    {
        this.HexBoardType = boardType;
        Create(size, cellsize);
    }

    public void Create(int size, float cellSize)
    {
        var holderName = "Game Board";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        var mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        this.Radius = size;
        this.Diameter = (2 * size) + 1;
        this.Tiles = new HexTile[Diameter, Diameter];
        this.CellSize = cellSize;

        for (var q = -Radius; q <= Radius; q++)
        {
            var r1 = Mathf.Max(-Radius, -q - Radius);
            var r2 = Mathf.Min(Radius, -q + Radius);
            for (var r = r1; r <= r2; r++)
            {
                var tile = CreateTile(q, r);
                tile.transform.parent = mapHolder;
                AddTileToMap(tile, q, r);
            }
        }
    }

    private HexTile CreateTile(int q, int r)
    {
        var tile = Instantiate(HexTile);
        if(HexBoardType == BoardType.FlatHexBoard)
        {
            tile.SetHexType(HexType.PointyTop);
        }
        else
        {
            tile.SetHexType(HexType.FlatTop);
        }
        tile.SetSize(CellSize);
        tile.SetColor(GetCellColor(q, r));
        tile.SetPosition(GetCellPosition(q, r, tile.Size));

        return tile;
    }

    private Color GetCellColor(int letterCol, int numberRow)
    {
        var outputColor = letterCol % 3;
        outputColor += numberRow % 3;

        return (outputColor == 0) ? Constants.MediumColor : (outputColor == 1) ? Constants.DarkColor : Constants.LightColor;
    }

    private Vector2 GetCellPosition(int q, int r, Size fullSize)
    {
        var tileSize = fullSize / 2;
        var pos = transform.position;
        float wSpacing, hSpacing, xPos, yPos;
        if(HexBoardType == BoardType.PointyHexBoard)
        {
            wSpacing = tileSize.W * 0.65f;
            hSpacing = tileSize.H * 0.75f;
            xPos = q * wSpacing;
            yPos = (r * hSpacing) + (q * (0.5f * hSpacing));
        }
        else
        {
            wSpacing = tileSize.W * 0.75f;
            hSpacing = tileSize.H * 0.65f;
            xPos = (q * wSpacing) + (r * (0.5f * wSpacing));
            yPos = r * hSpacing;
        }

        return new Vector2(pos.x + xPos, pos.y + yPos);
    }

    private void AddTileToMap(HexTile tile, int q, int r)
    {
        var x = Radius + q;
        var y = Radius + r;
        Tiles[x, y] = tile;
    }

    public Size GetBoardSize()
    {
        throw new System.NotImplementedException();
    }

    public TileStatusResponse GetTileStatus(Coord coord)
    {
        throw new System.NotImplementedException();
    }

    public Vector2 GetTilePosition(Coord coord)
    {
        throw new System.NotImplementedException();
    }

    public float GetCellSize()
    {
        throw new System.NotImplementedException();
    }
}

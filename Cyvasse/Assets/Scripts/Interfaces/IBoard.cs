using UnityEngine;
using System.Collections;

public interface IBoard
{
    void Create(int size, float cellSize);
    Size GetBoardSize();
    TileStatusResponse GetTileStatus(Coord coord);
    Vector2 GetTilePosition(Coord coord);
    float GetCellSize();
}

public struct TileStatusResponse
{
    public IPiece Piece { get; set; }
    public TileStatus Status { get; set; }
}

public enum TileStatus
{
    Empty,
    Occupied,
    DoesNotExist,
    InvalidMovement
}

public enum Sides
{
    White,
    Black
}
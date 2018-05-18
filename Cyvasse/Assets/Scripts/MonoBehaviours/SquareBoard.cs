using UnityEngine;
using System.Collections;

public class SquareBoard : MonoBehaviour, IBoard
{
    public Tile SquareTile;
    public Pawn SamplePawn;
    public Rook SampleRook;
    public Bishop SampleBishop;
    public Knight SampleKnight;
    public Queen SampleQueen;
    public King SampleKing;

    public float CellSize { get; set; }
    public int Size { get; set; }

    private Tile[,] Tiles { get; set; }
    private BasePiece[] Pieces { get; set; }

    public void Create(int size, float cellSize)
    {
        var holderName = "Game Board";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        var mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        this.Tiles = new Tile[size,size];
        this.Size = size;
        this.CellSize = cellSize;
        
        for(var i = 0; i < Tiles.GetLength(0); i++)
        {
            for(var j = 0; j < Tiles.GetLength(1); j++)
            {
                var tile = CreateTile(i, j);
                tile.transform.parent = mapHolder;
                Tiles[i, j] = tile;
            }
        }

        //Pieces At Starting Positions
        foreach(var pos in _startingPositions)
        {
            foreach (var coord in pos.Positions)
            {
                BasePiece piece = GetPiece(pos.PieceType);
                piece.transform.parent = mapHolder;
                piece.RegisterBoard(this);
                piece.SetSide(pos.Side);
                piece.SetCoordinates(coord);
            }
        }
    }

    private BasePiece GetPiece(string pieceType)
    {
        switch (pieceType)
        {
            case "Rook":
                var rook = Instantiate(SampleRook);
                return rook;
            case "Knight":
                var knight = Instantiate(SampleKnight);
                return knight;
            case "Bishop":
                var bishop = Instantiate(SampleBishop);
                return bishop;
            case "Queen":
                var queen = Instantiate(SampleQueen);
                return queen;
            case "King":
                var king = Instantiate(SampleKing);
                return king;
            default:
                var pawn = Instantiate(SamplePawn);
                return pawn;
        }
    }

    public Vector2 GetTilePosition(Coord coord)
    {
        if(!IsCoordInBounds(coord))
        {
            return Vector2.negativeInfinity;
        }

        var tile = Tiles[coord.X, coord.Y];

        return (tile != null) ? tile.GetPosition() : Vector2.negativeInfinity;
    }

    public float GetCellSize()
    {
        return CellSize;
    }

    public Size GetBoardSize()
    {
        return new Size { Height = Size, Width = Size };
    }

    public TileStatusResponse GetTileStatus(Coord coord)
    {
        if(!IsCoordInBounds(coord))
        {
            return new TileStatusResponse { Status = TileStatus.DoesNotExist };
        }

        var tile = Tiles[coord.X, coord.Y];
        if(tile == null)
        {
            return new TileStatusResponse { Status = TileStatus.DoesNotExist };
        }

        //Check if the move would put king in check

        foreach(var piece in Pieces)
        {
            if(piece.GetCoordinates() == coord)
            {
                return new TileStatusResponse { Status = TileStatus.Occupied, Piece = piece };
            }
        }

        return new TileStatusResponse { Status = TileStatus.Empty };
    }

    public bool IsCoordInBounds(Coord coord)
    {
        return coord.X > -1 && coord.Y > -1 && coord.X < Tiles.GetLength(0) && coord.Y < Tiles.GetLength(1);
    }

    private Tile CreateTile(int letterCol, int numberRow)
    {
        var tile = Instantiate(SquareTile);
        tile.SetSize(CellSize);
        tile.SetColor(GetCellColor(letterCol, numberRow));
        tile.SetPosition(GetCellPosition(letterCol, numberRow));

        return tile;
    }

    private Color GetCellColor(int letterCol, int numberRow)
    {
        return ((letterCol + 1) % 2 == 0) ? 
                    ((numberRow + 1) % 2 == 0) ? Constants.DarkColor : Constants.LightColor : 
                    ((numberRow + 1) % 2 == 0) ? Constants.LightColor : Constants.DarkColor; 
    }

    private Vector2 GetCellPosition(int letterCol, int numberRow)
    {
        var pos = transform.position;
        var positionFactor = (Size / 2f) - 0.5f;
        return new Vector2(pos.x + (CellSize * (letterCol - positionFactor)), pos.y + (CellSize * (numberRow - positionFactor)));
    }

    private StartingPositions[] _startingPositions = new StartingPositions[] { new  StartingPositions { Side = Sides.White,
                                                                                                        PieceType = "Pawn",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(0, 1),
                                                                                                            new Coord(1, 1),
                                                                                                            new Coord(2, 1),
                                                                                                            new Coord(3, 1),
                                                                                                            new Coord(4, 1),
                                                                                                            new Coord(5, 1),
                                                                                                            new Coord(6, 1),
                                                                                                            new Coord(7, 1)
                                                                                                        } },
                                                                                new  StartingPositions { Side = Sides.Black,
                                                                                                        PieceType = "Pawn",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(0, 6),
                                                                                                            new Coord(1, 6),
                                                                                                            new Coord(2, 6),
                                                                                                            new Coord(3, 6),
                                                                                                            new Coord(4, 6),
                                                                                                            new Coord(5, 6),
                                                                                                            new Coord(6, 6),
                                                                                                            new Coord(7, 6)
                                                                                                        } },
                                                                                new StartingPositions { Side = Sides.White,
                                                                                                        PieceType = "Rook",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(0, 0),
                                                                                                            new Coord(7, 0)
                                                                                                        } },
                                                                                new StartingPositions { Side = Sides.White,
                                                                                                        PieceType = "Knight",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(1, 0),
                                                                                                            new Coord(6, 0)
                                                                                                        } },
                                                                                new StartingPositions { Side = Sides.White,
                                                                                                        PieceType = "Bishop",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(2, 0),
                                                                                                            new Coord(5, 0)
                                                                                                        } },
                                                                                new StartingPositions { Side = Sides.White,
                                                                                                        PieceType = "Queen",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(3, 0)
                                                                                                        } },
                                                                                new StartingPositions { Side = Sides.White,
                                                                                                        PieceType = "King",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(4, 0)
                                                                                                        } },
                                                                                new StartingPositions { Side = Sides.Black,
                                                                                                        PieceType = "Rook",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(0, 7),
                                                                                                            new Coord(7, 7)
                                                                                                        } },
                                                                                new StartingPositions { Side = Sides.Black,
                                                                                                        PieceType = "Knight",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(1, 7),
                                                                                                            new Coord(6, 7)
                                                                                                        } },
                                                                                new StartingPositions { Side = Sides.Black,
                                                                                                        PieceType = "Bishop",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(2, 7),
                                                                                                            new Coord(5, 7)
                                                                                                        } },
                                                                                new StartingPositions { Side = Sides.Black,
                                                                                                        PieceType = "Queen",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(3, 7)
                                                                                                        } },
                                                                                new StartingPositions { Side = Sides.Black,
                                                                                                        PieceType = "King",
                                                                                                        Positions = new Coord[] {
                                                                                                            new Coord(4, 7)
                                                                                                        } },
    };
}

public struct StartingPositions
{
    public Sides Side { get; set; }
    public string PieceType { get; set; }
    public Coord[] Positions { get; set; }
}

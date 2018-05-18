using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class King : BasePiece
{
    public override string GetLetterAbbreviation()
    {
        return "K";
    }

    public override IEnumerable<Movement> GetValidMovement()
    {
        var output = new List<Movement>();

        //Normal Movment
        var possibleMoves = new Coord[] { new Coord(1, -1),
                                          new Coord(1, 1),
                                          new Coord(-1, -1),
                                          new Coord(-1, 1),
                                          new Coord(-1, 0),
                                          new Coord(1, 0),
                                          new Coord(0, 1),
                                          new Coord(0, -1)
        };

        foreach (var coord in possibleMoves)
        {
            var possibleMove = _currentCoord + coord;
            var tileStatus = _board.GetTileStatus(possibleMove);
            switch (tileStatus.Status)
            {
                case TileStatus.Occupied:
                    if (tileStatus.Piece.GetSide() != GetSide())
                    {
                        output.Add(new Movement(this, possibleMove, true));
                    }
                    break;
                case TileStatus.Empty:
                    output.Add(new Movement(this, possibleMove, false));
                    break;
                case TileStatus.DoesNotExist:
                case TileStatus.InvalidMovement:
                default:
                    break;
            }
        }

        //Castling

        return output;
    }
}

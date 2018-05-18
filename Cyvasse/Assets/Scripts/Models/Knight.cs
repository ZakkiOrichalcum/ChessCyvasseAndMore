using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Knight : BasePiece
{
    public override string GetLetterAbbreviation()
    {
        return "N";
    }

    public override IEnumerable<Movement> GetValidMovement()
    {
        var output = new List<Movement>();
        var possibleMoves = new Coord[] { new Coord(2, -1),
                                          new Coord(2, 1),
                                          new Coord(-2, -1),
                                          new Coord(-2, 1),
                                          new Coord(-1, -2),
                                          new Coord(1, -2),
                                          new Coord(-1, 2),
                                          new Coord(1, 2)
        };
        
        foreach(var coord in possibleMoves)
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
                default:
                    break;
            }
        }

        return output;
    }
}

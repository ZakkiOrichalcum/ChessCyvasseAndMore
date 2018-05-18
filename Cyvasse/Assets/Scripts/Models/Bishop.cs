using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : BasePiece
{
    public override string GetLetterAbbreviation()
    {
        return "B";
    }

    public override IEnumerable<Movement> GetValidMovement()
    {
        var size = _board.GetBoardSize();
        var output = new List<Movement>();
        var patterns = 0;

        while(patterns < 4)
        {
            Coord searchDimensions = (patterns == 0) ? new Coord(-1, -1) :
                                     (patterns == 1) ? new Coord(-1, 1)  :
                                     (patterns == 2) ? new Coord(1, -1)  :
                                                       new Coord(1, 1);

            var multiplier = 1;
            var stopped = false;
            while(multiplier < size.W && !stopped)
            {
                var possibleMove = _currentCoord + (searchDimensions * multiplier);
                var tileStatus = _board.GetTileStatus(possibleMove);
                switch (tileStatus.Status)
                {
                    case TileStatus.Occupied:
                        if(tileStatus.Piece.GetSide() != GetSide())
                        {
                            output.Add(new Movement(this, possibleMove, true));
                        }
                        stopped = true;
                        break;
                    case TileStatus.Empty:
                        output.Add(new Movement(this, possibleMove, false));
                        break;
                    case TileStatus.InvalidMovement:
                        break;
                    case TileStatus.DoesNotExist:
                    default:
                        stopped = true;
                        break;
                }

                multiplier++;
            }

            patterns++;
        }

        return output;
    }
}

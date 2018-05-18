using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Queen : BasePiece
{
    public override string GetLetterAbbreviation()
    {
        return "Q";
    }

    public override IEnumerable<Movement> GetValidMovement()
    {
        var size = _board.GetBoardSize();
        var output = new List<Movement>();
        var patterns = 0;

        while (patterns < 8)
        {
            Coord searchDimensions = (patterns == 0) ? new Coord(-1, -1) :
                                     (patterns == 1) ? new Coord(-1, 1) :
                                     (patterns == 2) ? new Coord(1, -1) :
                                     (patterns == 3) ? new Coord(-1, -1) :
                                     (patterns == 4) ? new Coord(-1, 0) :
                                     (patterns == 5) ? new Coord(1, 0) :
                                     (patterns == 6) ? new Coord(0, -1) :
                                                       new Coord(0, 1);

            var multiplier = 1;
            var stopped = false;
            while (multiplier < size.W && !stopped)
            {
                var possibleMove = _currentCoord + (searchDimensions * multiplier);
                var tileStatus = _board.GetTileStatus(possibleMove);
                switch (tileStatus.Status)
                {
                    case TileStatus.Occupied:
                        if (tileStatus.Piece.GetSide() != GetSide())
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

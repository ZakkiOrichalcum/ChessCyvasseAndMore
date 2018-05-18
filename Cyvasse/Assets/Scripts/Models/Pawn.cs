using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pawn : BasePiece
{
    private Coord _startingPosition { get; set; }
    private bool _hasSetStartingPosition = false;

    public override void SetCoordinates(Coord coords)
    {
        if(!_hasSetStartingPosition)
        {
            _startingPosition = coords;
            _hasSetStartingPosition = true;
        }
        base.SetCoordinates(coords);
    }

    public string GetLetterAbbreviation(bool isAttacking)
    {
        if (isAttacking)
        {
            return Movement.Letters[_currentCoord.X];
        }
        else
        {
            return "";
        }
    }

    public override IEnumerable<Movement> GetValidMovement()
    {
        var output = new List<Movement>();

        var movementDirection = (GetSide() == Sides.White) ? new Coord(0, 1) : new Coord(0, -1);
        //Normal Movement
        var validNormal = _board.GetTileStatus(_currentCoord + movementDirection);
        if (validNormal.Equals(TileStatus.Empty))
        {
            output.Add(new Movement(this, _currentCoord + movementDirection, false));
        }

        //Starting Movement
        if(_currentCoord == _startingPosition)
        {
            var statusStarting = _board.GetTileStatus(_currentCoord + (movementDirection * 2));
            if (validNormal.Equals(TileStatus.Empty))
            {
                output.Add(new Movement(this, _currentCoord + (movementDirection * 2), false));
            }
        }

        //Attacking
        var attackLeftCoord = _currentCoord + new Coord(-1, 0);
        var attackRightCoord = _currentCoord + new Coord(1, 0);
        var leftStatus = _board.GetTileStatus(attackLeftCoord);
        if(leftStatus.Equals(TileStatus.Occupied) && leftStatus.Piece.GetSide() != GetSide())
        {
            output.Add(new Movement(this, attackLeftCoord, true));
        }

        var rightStatus = _board.GetTileStatus(attackRightCoord);
        if (rightStatus.Equals(TileStatus.Occupied) && rightStatus.Piece.GetSide() != GetSide())
        {
            output.Add(new Movement(this, attackRightCoord, true));
        }

        return output;
    }
}

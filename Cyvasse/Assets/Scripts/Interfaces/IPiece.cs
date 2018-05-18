using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPiece
{
    void RegisterBoard(IBoard board);
    void SetSide(Sides s);
    Sides GetSide();
    void SetCoordinates(Coord coords);
    Coord GetCoordinates();
    IEnumerable<Movement> GetValidMovement();
    string GetLetterAbbreviation();
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BasePiece : MonoBehaviour, IPiece
{
    public Sprite WhiteSprite;
    public Sprite BlackSprite;
    public SpriteRenderer renderer;

    protected IBoard _board { get; private set; }
    protected Coord _currentCoord { get; private set; }
    protected Sides _side { get; private set; }

    public virtual void RegisterBoard(IBoard board)
    {
        this._board = board;

        var spriteSize = renderer.sprite.bounds.size;
        var scaleFactorH = _board.GetCellSize() / spriteSize.y;
        var scaleFactorW = _board.GetCellSize() / spriteSize.x;
        transform.localScale = new Vector3(scaleFactorW, scaleFactorH, transform.localScale.z);
    }

    public void SetSide(Sides s)
    {
        _side = s;

        if (_side == Sides.White) renderer.sprite = WhiteSprite;
        else renderer.sprite = BlackSprite;
    }

    public Sides GetSide()
    {
        return _side;
    }

    public virtual void SetCoordinates(Coord coords)
    {
        _currentCoord = coords;

        if(_board != null)
        {
            var pos = _board.GetTilePosition(_currentCoord);
            if(!pos.Equals(Vector2.negativeInfinity))
            {
                transform.position = new Vector3(pos.x, pos.y, -0.1f);
            }
        }
    }

    public Coord GetCoordinates()
    {
        return _currentCoord;
    }

    public virtual IEnumerable<Movement> GetValidMovement()
    {
        throw new System.NotImplementedException();
    }

    public virtual string GetLetterAbbreviation()
    {
        throw new System.NotImplementedException();
    }
}

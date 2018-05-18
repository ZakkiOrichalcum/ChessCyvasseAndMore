using UnityEngine;
using System.Collections;

public class SquardTile : Tile
{
    public SpriteRenderer Square;

    public override void SetSize(float size)
    {
        var spriteSize = Square.sprite.bounds.size;
        var scaleFactorH = size / spriteSize.y;
        var scaleFactorW = size / spriteSize.x;
        transform.localScale = new Vector3(scaleFactorW, scaleFactorH, transform.localScale.z);
    }

    public override void SetColor(Color color)
    {
        Square.color = color;
    }
}

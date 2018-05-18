using UnityEngine;
using System.Collections;

public class HexTile : Tile
{
    public SpriteRenderer Hex;
    public HexType HexType;

    public override void SetSize(float size)
    {
        //if(HexType == HexType.FlatTop)
        //{
        //    this.Size = new Size
        //    {
        //        Height = size,
        //        Width = Mathf.Sqrt(3) * (size / 2)
        //    };
        //}
        //else if(HexType == HexType.PointyTop)
        //{
        //    this.Size = new Size
        //    {
        //        Height = Mathf.Sqrt(3) * (size / 2),
        //        Width = size
        //    };
        //}
        this.Size = new Size
        {
            Height = size,
            Width = size
        };
        transform.localScale = new Vector3(Size.W, Size.H, transform.localScale.z);
    }

    public override void SetColor(Color color)
    {
        Hex.color = color;
    }

    public void SetHexType(HexType hexType)
    {
        this.HexType = hexType;
        if(HexType == HexType.PointyTop)
        {
            Hex.transform.Rotate(0, 0, 90);
        }
    }
}

public enum HexType
{
    FlatTop,
    PointyTop
}
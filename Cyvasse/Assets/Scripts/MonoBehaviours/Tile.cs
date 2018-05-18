using UnityEngine;
using System;
using System.Collections;

public abstract class Tile : MonoBehaviour
{
    public Size Size { get; protected set; }

    public virtual void SetSize(float size)
    {
        throw new NotImplementedException();
    }

    public virtual void SetColor(Color color)
    {
        throw new NotImplementedException();
    }

    public virtual void SetPosition(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    public virtual void SetPosition(Vector3 pos3)
    {
        transform.position = pos3;
    }

    public virtual Vector2 GetPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
}

public struct Size
{
    public float Height { get; set; }
    public float Width { get; set; }
    public float H {
        get { return Height; }
        set { Height = value; }
    }
    public float W
    {
        get { return Width; }
        set { Width = value; }
    }

    public static Size operator / (Size size, float divisor)
    {
        return new Size
        {
            Height = size.H / divisor,
            Width = size.W / divisor
        };
    }
}

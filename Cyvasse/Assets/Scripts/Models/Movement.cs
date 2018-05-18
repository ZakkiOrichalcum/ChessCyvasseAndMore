using System.Text;

public class Movement
{
    public Coord Coordinates { get; set; }
    public IPiece Piece { get; set; }
    public bool IsACheck { get; set; }
    public bool DidCapture { get; set; }

    public Movement()
    { }
    public Movement(IPiece p, Coord c, bool capture)
    {
        this.Piece = p;
        this.Coordinates = c;
        this.DidCapture = capture;
    }

    public static Movement NotationToMovement(string notation)
    {
        var output = new Movement();
        if (notation.Contains("x"))
        {
            output.DidCapture = true;
            notation = notation.Replace("x", "");
        }
        if (notation.Contains("+"))
        {
            output.IsACheck = true;
            notation = notation.Replace("+", "");
        }
        output.Piece = GetPiece(notation.Substring(0, 1));
        notation = notation.Substring(1);
        output.Coordinates = GetCoords(notation.Substring(0, 1), notation.Substring(1));

        return output;
    }

    public static string[] Letters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l" };

    public static string MovementToNotation(Movement movement)
    {
        var output = new StringBuilder();
        if(movement.Piece.GetType() == typeof(Pawn))
        {
            output.Append((movement.Piece as Pawn).GetLetterAbbreviation(movement.DidCapture));
        }
        else
        {
            output.Append(movement.Piece.GetLetterAbbreviation());
        }
        if (movement.DidCapture) output.Append("x");
        output.Append(Letters[movement.Coordinates.X]);
        output.Append(movement.Coordinates.Y + 1);
        if (movement.IsACheck) output.Append("+");

        return output.ToString();
    }

    public static IPiece GetPiece(string notation)
    {
        switch (notation)
        {
            case "R":
                return new Rook();
            case "B":
                return new Bishop();
            case "N":
                return new Knight();
            case "Q":
                return new Queen();
            case "K":
                return new King();
            default:
                return new Pawn();
        }
    }

    public static Coord GetCoords(string letterCol, string row)
    {
        var x = -1; var i = 0;
        while(x < 0 && i < Letters.Length)
        {
            if(letterCol == Letters[i])
            {
                x = i;
            }

            i++;
        }

        return new Coord(x, int.Parse(row) - 1);
    }
}

public class CastlingMovment : Movement
{
    public bool IsQueenSide { get; set; }
}

public class Coord
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coord(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static bool operator == (Coord c1, Coord c2)
    {
        return c1.X == c2.X && c1.Y == c2.Y;
    }

    public override bool Equals(object obj)
    {
        return (obj.GetType() == typeof(Coord)) ? this == (Coord)obj : false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator != (Coord c1, Coord c2)
    {
        return !(c1 == c2);
    }

    public static Coord operator +(Coord c1, Coord c2)
    {
        return new Coord (c1.X + c2.X, c1.Y + c2.Y );
    }

    public static Coord operator +(Coord c, int i)
    {
        return new Coord(c.X + i, c.Y + i);
    }

    public static Coord operator *(Coord c, int i)
    {
        return new Coord(c.X * i, c.Y * i);
    }
}
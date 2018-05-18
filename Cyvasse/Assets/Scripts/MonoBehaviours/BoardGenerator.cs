using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour {

    public SquareBoard _squareboard;
    public HexBoard _hexboard;
    public BoardType Boards = BoardType.SquareBoard;
    public float cellSize = 30;
    public int size = 8;

    private void Start()
    {
        GenerateBoard();
    }

    public void GenerateBoard()
    {
        var holderName = "Board Holder";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        if (Boards == BoardType.SquareBoard)
        {
            var mapHolder = Instantiate(_squareboard);
            mapHolder.name = holderName;
            mapHolder.transform.parent = transform;
            mapHolder.Create(size, cellSize);
        }
        else
        {
            var mapHolder = Instantiate(_hexboard);
            mapHolder.name = holderName;
            mapHolder.transform.parent = transform;
            mapHolder.Create(Boards, size, cellSize);
        }
    }
}

public enum BoardType
{
    SquareBoard,
    FlatHexBoard,
    PointyHexBoard
}
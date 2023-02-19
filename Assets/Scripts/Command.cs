using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    //store an history of moves
    public static Stack<Command> history = new Stack<Command>();
    //store an history of undone moves
    public static Stack<Command> undoneHistory = new Stack<Command>();

    //Store the last move that was made
    public bool isX;
    public Vector2Int position;

    public Command(bool isX, Vector2Int position)
    {
        this.isX = isX;
        this.position = position;
        history.Push(this);
    }

    public static void Undo()
    {
        if (history.Count == 0)
        {
            Debug.Log("Moves history is empty");
            return;
        }

        Command command = history.Pop();

        if (command == history.Peek())
        {
            Debug.Log("WHHHHHHY");
        }

        //revert the turn
        GameManager.Instance.GameState.SetTurn(!command.isX);
        //revert the move
        GameManager.Instance.Board.OverideSymbol(command.position, Symobl.EMPTY);

        //move this move to undone
        undoneHistory.Push(command);
    }

    public static void Redo()
    {
        if (undoneHistory.Count == 0)
        {
            return;
        }

        //get the command to redo
        Command command = undoneHistory.Pop();

        //redo the move
        GameManager.Instance.GameState.SetTurn(command.isX);

        Symobl symobl = command.isX ? Symobl.X : Symobl.O;

        GameManager.Instance.Board.SetSymbol(command.position, symobl);
    }

}

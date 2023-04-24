using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
  abstract class Handler
  {
    public Handler Successor { get; set; }
    public abstract void HandleRequest(int condition, ConsoleApp1.SquareMatrix MatrixA, ConsoleApp1.SquareMatrix MatrixB);
  }

  class Multiplication : Handler
  {
    public override void HandleRequest(int condition, ConsoleApp1.SquareMatrix MatrixA, ConsoleApp1.SquareMatrix MatrixB)
    {
      ConsoleApp1.SquareMatrix Result = MatrixA * MatrixB;
      Console.WriteLine("Умножение выполнено");

      if (condition == 1)
      {
        Result.Display();
      }
      else if (Successor != null)
      {
        Successor.HandleRequest(condition, Result, MatrixB);
      }
    }
  }

  class Addition : Handler
  {
    public override void HandleRequest(int condition, ConsoleApp1.SquareMatrix MatrixA, ConsoleApp1.SquareMatrix MatrixB)
    {
      ConsoleApp1.SquareMatrix Result = MatrixA + MatrixB;
      Console.WriteLine("Сложение выполнено");

      if (condition == 2)
      {
        Result.Display();
      }
      else if (Successor != null)
      {
        Successor.HandleRequest(condition, Result, MatrixB);
      }
    }
  }

  class Diagonalization : Handler
  {
    public override void HandleRequest(int condition, ConsoleApp1.SquareMatrix MatrixA, ConsoleApp1.SquareMatrix MatrixB)
    {
      ConsoleApp1.SquareMatrix Result = MatrixA.ToDiagonal(MatrixA);
      Console.WriteLine("Диаганализация выполнена");

      if (condition == 3)
      {
        Result.Display();
      }
      else if (Successor != null)
      {
        Successor.HandleRequest(condition, MatrixA, MatrixB);
      }
    }
  }
}

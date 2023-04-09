using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
  class SquareMatrix : ICloneable
  {
    private int Dimension;
    public SquareMatrix(int Dimension)
    {
      if (Dimension <= 0)
      {
        throw new InvalidMatrixException(Dimension.ToString());
      }
      else
      {
        this.Dimension = Dimension;
      }
    }

    public int[,] Matrix = new int[100, 100];

    public delegate SquareMatrix Diagonalization(SquareMatrix Matrix);
    public Diagonalization ToDiagonal = delegate (SquareMatrix Matrix)
    {
      SquareMatrix Result = Matrix;

      for (int mainE = 0; mainE < Result.Dimension; ++mainE)
      {
        for (int i = 0; i < Matrix.Dimension; ++i)
        {
          if (i != mainE)
          {
            for (int j = 0; j < Matrix.Dimension; ++j)
            {
              if (j != mainE)
              {
                Result.Matrix[i, j] = (Result.Matrix[i, j] * Result.Matrix[mainE, mainE]) - (Result.Matrix[i, mainE] * Result.Matrix[mainE, j]);
              }
            }
          }
        }

        for (int i = 0; i < Matrix.Dimension; ++i)
        {
          if (i != mainE)
          {
            Result.Matrix[mainE, i] = 0;
          }
        }
      }

      for (int d = 0; d < Result.Dimension; ++d)
      {
        if (Result.Matrix[d, d] == 0)
        {
          return Result;
        }
      }

      bool key = true;
      for (int d = 0; d < Result.Dimension; ++d)
      {
        if (Result.Matrix[d, d] % Result.Matrix[Result.Dimension - 1, Result.Dimension - 1] != 0)
        {
          key = false;
        }
      }
      if (key = true)
      {
        for (int d = 0; d < Result.Dimension; ++d)
        {
          Result.Matrix[d, d] = Result.Matrix[d, d] / Result.Matrix[Result.Dimension - 1, Result.Dimension - 1];
        }
      }
      return Result;
    };

    public void CreateMatrix()
    {
      Random RandomNumber = new Random();
      for (int RowIndex1 = 0; RowIndex1 < Dimension; ++RowIndex1)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < Dimension; ++ClolumnIndex)
        {
          Matrix[RowIndex1, ClolumnIndex] = RandomNumber.Next(0, 10);
        }
      }
    }

    public object Clone()
    {
      SquareMatrix DeepCloneMatrix = new SquareMatrix(this.Dimension);
      return DeepCloneMatrix;
    }

    public static SquareMatrix operator +(SquareMatrix MatrixA, SquareMatrix MatrixB)
    {
      SquareMatrix Answer = new SquareMatrix(MatrixA.Dimension);
      for (int RowIndex1 = 0; RowIndex1 < MatrixA.Dimension; ++RowIndex1)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < MatrixA.Dimension; ++ClolumnIndex)
        {
          Answer.Matrix[RowIndex1, ClolumnIndex] = MatrixA.Matrix[RowIndex1, ClolumnIndex] + MatrixB.Matrix[RowIndex1, ClolumnIndex];
        }
      }
      return Answer;
    }

    public static SquareMatrix operator *(SquareMatrix MatrixA, SquareMatrix MatrixB)
    {
      SquareMatrix Answer = new SquareMatrix(MatrixA.Dimension);
      for (int RowIndex1 = 0; RowIndex1 < MatrixA.Dimension; ++RowIndex1)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < MatrixA.Dimension; ++ClolumnIndex)
        {
          for (int MultiplicationIndex = 0; MultiplicationIndex < MatrixA.Dimension; ++MultiplicationIndex)
          {
            Answer.Matrix[RowIndex1, ClolumnIndex] = MatrixA.Matrix[RowIndex1, MultiplicationIndex] * MatrixB.Matrix[MultiplicationIndex, ClolumnIndex];
          }
        }
      }
      return Answer;
    }

    public static explicit operator double[][](SquareMatrix MatrixA)
    {
      double[][] Answer = new double[MatrixA.Dimension][];

      for (int RowIndex = 0; RowIndex < MatrixA.Dimension; ++RowIndex)
      {
        Answer[RowIndex] = new double[MatrixA.Dimension];
      }

      for (int RowIndex = 0; RowIndex < MatrixA.Dimension; ++RowIndex)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < MatrixA.Dimension; ++ClolumnIndex)
        {
          Answer[RowIndex][ClolumnIndex] = MatrixA.Matrix[RowIndex, ClolumnIndex];
        }
      }
      return Answer;
    }

    public static bool operator >(SquareMatrix MatrixA, SquareMatrix MatrixB)
    {
      double[][] doubleMatrix1 = (double[][])MatrixA;
      double[][] doubleMatrix2 = (double[][])MatrixB;
      double detA = ConsoleApp2.Gaus.MatrixDeterminant(doubleMatrix1);
      double detB = ConsoleApp2.Gaus.MatrixDeterminant(doubleMatrix2);

      return (detA > detB);
    }

    public static bool operator <(SquareMatrix MatrixA, SquareMatrix MatrixB)
    {
      double[][] doubleMatrix1 = (double[][])MatrixA;
      double[][] doubleMatrix2 = (double[][])MatrixB;
      double detA = ConsoleApp2.Gaus.MatrixDeterminant(doubleMatrix1);
      double detB = ConsoleApp2.Gaus.MatrixDeterminant(doubleMatrix2);

      return (detA < detB);
    }

    public static bool operator >=(SquareMatrix MatrixA, SquareMatrix MatrixB)
    {
      double[][] doubleMatrix1 = (double[][])MatrixA;
      double[][] doubleMatrix2 = (double[][])MatrixB;
      double detA = ConsoleApp2.Gaus.MatrixDeterminant(doubleMatrix1);
      double detB = ConsoleApp2.Gaus.MatrixDeterminant(doubleMatrix2);

      return (detA >= detB);
    }

    public static bool operator <=(SquareMatrix MatrixA, SquareMatrix MatrixB)
    {
      double[][] doubleMatrix1 = (double[][])MatrixA;
      double[][] doubleMatrix2 = (double[][])MatrixB;
      double detA = ConsoleApp2.Gaus.MatrixDeterminant(doubleMatrix1);
      double detB = ConsoleApp2.Gaus.MatrixDeterminant(doubleMatrix2);

      return (detA <= detB);
    }

    public static bool operator ==(SquareMatrix MatrixA, SquareMatrix MatrixB)
    {
      for (int RowIndex1 = 0; RowIndex1 < MatrixA.Dimension; ++RowIndex1)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < MatrixA.Dimension; ++ClolumnIndex)
        {
          if (MatrixA.Matrix[RowIndex1, ClolumnIndex] != MatrixB.Matrix[RowIndex1, ClolumnIndex])
          {
            return false;
          }
        }
      }
      return true;
    }

    public static bool operator !=(SquareMatrix MatrixA, SquareMatrix MatrixB)
    {
      for (int RowIndex1 = 0; RowIndex1 < MatrixA.Dimension; ++RowIndex1)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < MatrixA.Dimension; ++ClolumnIndex)
        {
          if (MatrixA.Matrix[RowIndex1, ClolumnIndex] == MatrixB.Matrix[RowIndex1, ClolumnIndex])
          {
            return false;
          }
        }
      }
      return true;
    }

    public static bool operator true(SquareMatrix MatrixA)
    {
      for (int RowIndex1 = 0; RowIndex1 < MatrixA.Dimension; ++RowIndex1)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < MatrixA.Dimension; ++ClolumnIndex)
        {
          if (MatrixA.Matrix[RowIndex1, ClolumnIndex] == 0)
          {
            return false;
          }
        }
      }
      return true;
    }

    public static bool operator false(SquareMatrix MatrixA)
    {
      for (int RowIndex1 = 0; RowIndex1 < MatrixA.Dimension; ++RowIndex1)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < MatrixA.Dimension; ++ClolumnIndex)
        {
          if (MatrixA.Matrix[RowIndex1, ClolumnIndex] != 0)
          {
            return false;
          }
        }
      }
      return true;
    }

    public static double Detdeterminant(SquareMatrix MatrixA)
    {
      double Answer;
      double[][] mat = (double[][])MatrixA;
      Answer = ConsoleApp2.Gaus.MatrixDeterminant(mat);
      return Answer;
    }

    public static double[][] InversesMatrix(SquareMatrix MatrixA)
    {
      double[][] Answer = new double[MatrixA.Dimension][];
      for (int RowIndex = 0; RowIndex < MatrixA.Dimension; ++RowIndex)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < MatrixA.Dimension; ++ClolumnIndex)
        {
          Answer[RowIndex][ClolumnIndex] = MatrixA.Matrix[ClolumnIndex, RowIndex];
        }
      }

      double Detdeterminant = ConsoleApp2.Gaus.MatrixDeterminant(Answer);

      for (int RowIndex = 0; RowIndex < MatrixA.Dimension; ++RowIndex)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < MatrixA.Dimension; ++ClolumnIndex)
        {
          Answer[RowIndex][ClolumnIndex] /= Detdeterminant;
        }
      }
      return Answer;
    }

    public override bool Equals(Object obj1)
    {
      if (obj1 is SquareMatrix)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public override int GetHashCode()
    {
      return (int)this.Dimension;
    }

    public override string ToString()
    {
      string strResult = "";

      for (int RowIndex = 0; RowIndex < Dimension; ++RowIndex)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < Dimension; ++ClolumnIndex)
        {
          strResult += Matrix[RowIndex, ClolumnIndex].ToString();
        }
      }
      return strResult;
    }

    public int CompareTo(object other)
    {
      if (other is SquareMatrix)
      {
        if (Matrix[0, 0] > Matrix[1, 0]) return -1;
        if (Matrix[0, 0] == Matrix[1, 0]) return 0;
        if (Matrix[0, 0] < Matrix[1, 0]) return 1;
      }
      return -1;
    }

    public SquareMatrix Transposition(SquareMatrix Matrix)
    {
      SquareMatrix Result = new SquareMatrix(Matrix.Dimension);

      for (int RowIndex = 0; RowIndex < Matrix.Dimension; ++RowIndex)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < Matrix.Dimension; ++ClolumnIndex)
        {
          Result.Matrix[RowIndex, ClolumnIndex] = Matrix.Matrix[ClolumnIndex, RowIndex];
        }
      }
      return Result;
    }

    public int Trace(SquareMatrix Matrix)
    {
      int Trace = 0;

      for (int RowIndex = 0; RowIndex < Matrix.Dimension; ++RowIndex)
      {
        Trace += Matrix.Matrix[RowIndex, RowIndex];
      }

      return Trace;
    }

    public void Display()
    {
      for (int RowIndex1 = 0; RowIndex1 < Dimension; ++RowIndex1)
      {
        for (int ClolumnIndex = 0; ClolumnIndex < Dimension; ++ClolumnIndex)
        {
          Console.Write(Matrix[RowIndex1, ClolumnIndex] + " ");
        }
        Console.WriteLine("\n");
      }
    }
  }

  public class InvalidMatrixException : System.Exception
  {
    public InvalidMatrixException(string WrongDimension)
        : base("невозможно создать матрицу размером " + WrongDimension) { }
  }

  public class Singleton
  {
    public static Singleton Instance
    {
      get
      {
        if (instance == null) instance = new Singleton();
        return instance;
      }
    }
    public void MatrixCalculator()
    {
      int Dimension;
      bool Key = true;
      string Operation;

      Console.WriteLine("укажите размерность матриц: ");
      Dimension = Convert.ToInt32(Console.ReadLine());

      while (Key)
      {
        Key = false;
        try
        {
          SquareMatrix TextMatrix = new SquareMatrix(Dimension);
        }
        catch (InvalidMatrixException Error)
        {
          Console.WriteLine("Ошибка: " + Error.Message);
          Key = true;
          Dimension = Convert.ToInt32(Console.ReadLine());
        }
      }

      Console.WriteLine("первая матрица:");
      SquareMatrix matrix1 = new SquareMatrix(Dimension);

      matrix1.CreateMatrix();
      matrix1.Display();

      Console.WriteLine("вторая матрица:");
      SquareMatrix matrix2 = (SquareMatrix)matrix1.Clone();

      matrix2.CreateMatrix();
      matrix2.Display();

      Console.WriteLine("+, *, determ, invers, >, <, >=, <=, ==, !=, true, false, equals, hash, string, compare, transpos, trace");
      Console.WriteLine("какую операцию необходимо выполнить: ");
      Operation = Convert.ToString(Console.ReadLine());

      switch (Operation)
      {
        case "+":
          SquareMatrix SumMatrix = matrix1 + matrix2;
          SumMatrix.Display();
          break;

        case "*":
          SquareMatrix MultipliedMatrix = matrix1 * matrix2;
          MultipliedMatrix.Display();
          break;

        case "determ":
          double det1 = SquareMatrix.Detdeterminant(matrix1);
          Console.WriteLine(det1);
          break;

        case "invers":
          double[][] invers = SquareMatrix.InversesMatrix(matrix1);
          for (int RowIndex1 = 0; RowIndex1 < Dimension; ++RowIndex1)
          {
            for (int ClolumnIndex = 0; ClolumnIndex < Dimension; ++ClolumnIndex)
            {
              Console.Write(invers[RowIndex1][ClolumnIndex] + " ");
            }
            Console.WriteLine("\n");
          }
          break;

        case ">":
          Console.WriteLine(matrix1 > matrix2);
          break;

        case "<":
          Console.WriteLine(matrix1 < matrix2);
          break;

        case ">=":
          Console.WriteLine(matrix1 >= matrix2);
          break;

        case "<=":
          Console.WriteLine(matrix1 <= matrix2);
          break;

        case "==":
          Console.WriteLine(matrix1 == matrix2);
          break;

        case "!=":
          Console.WriteLine(matrix1 != matrix2);
          break;

        case "true":
          if (matrix1)
          {
            Console.WriteLine(true);
          }
          else
          {
            Console.WriteLine(false);
          }
          break;

        case "false":
          if (matrix1)
          {
            Console.WriteLine(false);
          }
          else
          {
            Console.WriteLine(true);
          }
          break;

        case "equals":
          Console.WriteLine(Equals(matrix1));
          break;

        case "hash":
          Console.WriteLine(GetHashCode());
          break;

        case "string":
          Console.WriteLine(matrix1.ToString());
          break;

        case "compare":
          Console.WriteLine(matrix2.CompareTo(matrix1));
          break;

        case "transpos":
          matrix1.Transposition(matrix1).Display();
          break;

        case "trace":
          Console.WriteLine(matrix1.Trace(matrix1));
          break;

        case "1":
          SquareMatrix Result = matrix1.ToDiagonal(matrix1);
          Result.Display();
          break;

        default:
          Console.WriteLine("некорректно указана операция");
          break;
      }
    }
    private Singleton() { }
    private static Singleton instance;
  }

  class Program
  {
    static void Main(string[] args)
    {
      Singleton.Instance.MatrixCalculator();
      Console.ReadKey();
    }
  }
}

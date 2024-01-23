
using System;
using System.Collections.Generic;

class Solver
{
    public static void Main()
    {

        int[,] goalState = new int[,]
        {
            {1, 2, 3},
            {8, 0, 4},
            {7, 6, 5}
        };

        int[,] startState = new int[,]
        {
            {2, 6, 3},
            {1, 4, 0},
            {8, 7, 5}
        };

        V result = SolvePuzzle(startState, goalState);
        result.Show();
    }

    static V SolvePuzzle(int[,] startState, int[,] goalState)
    {
        V start = new V(startState, 0);
        start.Show();
        Queue<V> queue = Next(start);
        while (queue != null)
        {
            List<V> next = new List<V>();
            int minF = int.MaxValue;
            foreach (var v in queue)
            {
                if(minF > v.F)
                {
                    minF = v.F;
                    
                }
            }
            foreach (var v in queue)
            {
                if (v.F == minF)
                {
                    next.Add(v);
                }
            }
            if(next.Count == 1)
            {
                if (next[0] != null && next[0].IsGoal)
                {
                    return next[0];
                }
                else
                {
                    Console.WriteLine($"Результат этапа {next[0].D} ");
                    next[0].Show();
                    Console.WriteLine("-----------");
                    queue = Next(next[0]);
                }
            }
            else
            {
                queue = Next(next[0]);
                for (int i = 1; i < next.Count; i++)
                {
                    var que = Next(next[i]);
                    foreach (var v in que)
                    {
                        queue.Enqueue(v);
                    }
                }
            }
            
            
        }
        return start;
    }
    
    public static Queue<V> Next(V v)
    {
        int[,] matrix = v.Value;
        int D = v.D;
        Queue<V> q = new Queue<V>();
        for(int i = 0;i < 3; i++)
        {
            for(int j = 0;j < 3; j++)
            {
                if (matrix[i, j] == 0)
                {
                    int tmp = matrix[i, j];
                    int[,] newMatrix = (int[,])matrix.Clone();
                    Console.WriteLine($"Номер этапа {D + 1}");
                    if (i + 1 <= 2)
                    {
                        newMatrix[i, j] = newMatrix[i + 1, j];
                        newMatrix[i + 1, j] = tmp;
                        V next = new V(newMatrix, D + 1, v.Value);
                        if (!next.Equals(v))
                        {
                            q.Enqueue(next);
                            next.Show();
                        }
                        newMatrix = (int[,])matrix.Clone();

                    }

                    if (i - 1 >= 0)
                    {
                        newMatrix[i, j] = newMatrix[i - 1, j];
                        newMatrix[i - 1, j] = tmp;
                        V next = new V(newMatrix, D + 1, v.Value);
                        if (!next.Equals(v))
                        {
                            q.Enqueue(next);
                            next.Show();
                        }
                        newMatrix = (int[,])matrix.Clone();
                    }
                    
                    if(j + 1 <= 2)
                    {
                        newMatrix[i, j] = newMatrix[i, j + 1];
                        newMatrix[i, j + 1] = tmp;
                        V next = new V(newMatrix, D + 1, v.Value);
                        if (!next.Equals(v))
                        {
                            q.Enqueue(next);
                            next.Show();
                        }
                        newMatrix = (int[,])matrix.Clone();
                        
                    }
                    
                    if (j - 1 >= 0)
                    {
                        newMatrix[i, j] = newMatrix[i, j - 1];
                        newMatrix[i, j - 1] = tmp;
                        V next = new V(newMatrix, D + 1, v.Value);
                        if (!next.Equals(v))
                        {
                            q.Enqueue(next);
                            next.Show();
                        }
                        

                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    break;
                }
            }
        }
        return q;
    }
}

class V
{
    public int[,] Value { get; set; }
    public int[,] Prev { get; set; }
    public int D { get; set; }
    public int K { get; set; }
    public int F
    {
        get
        {
            return D + K;
        }
    }
    public bool IsGoal { get; set; }


    private static int[,] goalState = new int[,]
    {
            {1, 2, 3},
            {8, 0, 4},
            {7, 6, 5}
    };
    public V(int[,] Value, int D, int[,] prev = null)
    {
        this.Value = Value;
        this.Prev = prev;
        this.D = D;
        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Value[i, j] != goalState[i, j] && Value[i, j] != 0)
                {
                    count++;
                }
            }
        }
        this.K = count;
        if (count == 0)
        {
            this.IsGoal = true;
        }
        Prev = prev;
    }
    public void Show()
    {
        Console.WriteLine($"Значения D K и F {D} {K} {F}") ;
        for (int i = 0; i < 3;i++) 
        {
            for(int j = 0; j < 3;j++)
            {
                Console.Write(Value[i, j] + " ");
            }
            Console.WriteLine();
        }

        
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        V other = (V)obj;
        if(other.Prev == null)
        {
            return false;
        }
        // Сравнение массивов Value
        return Enumerable.Range(0, 3)
            .All(i => Enumerable.Range(0, 3)
                .All(j => Value[i, j] == other.Prev[i, j]));
    }

    
}
using System;

class Box
{
    public int i, j;
    public int pre;
}

class QueueType
{
    public Box[] data;
    public int front, rear;

    public QueueType(int size)
    {
        data = new Box[size];
        front = rear = -1;
    }
}

class MazeSolver
{
    static int M, N;
    static int[,] mg;

    static void InitQueue(QueueType qu)
    {
        qu.front = qu.rear = -1;
    }

    static bool QueueEmpty(QueueType qu)
    {
        return qu.front == qu.rear;
    }

    static void EnQueue(QueueType qu, Box e)
    {
        qu.rear++;
        qu.data[qu.rear] = new Box { i = e.i, j = e.j, pre = e.pre };
    }

    static void DeQueue(QueueType qu, out Box e)
    {
        qu.front++;
        e = qu.data[qu.front];
    }

    static void DestroyQueue(QueueType qu)
    {
        qu.data = null;
    }

    static void Print(QueueType qu, int front)
    {
        int k = front;
        int ns = 0;
        Console.WriteLine("迷宫路径：");
        do
        {
            ns++;
            Console.Write($"({qu.data[k].i},{qu.data[k].j})");
            if (ns % 5 == 0) Console.WriteLine();
            else Console.Write(" -> ");
            k = qu.data[k].pre;
        } while (k != -1);
        Console.WriteLine();
    }

    static bool MazePath(int xi, int yi, int xe, int ye)
    {
        Box e;
        int i, j, di, i1, j1;
        int maxSize = (M + 2) * (N + 2);
        QueueType qu = new QueueType(maxSize);
        InitQueue(qu);

        e = new Box();
        e.i = xi; e.j = yi; e.pre = -1;
        EnQueue(qu, e);
        mg[xi, yi] = -1;

        while (!QueueEmpty(qu))
        {
            DeQueue(qu, out e);
            i = e.i; j = e.j;

            if (i == xe && j == ye)
            {
                Print(qu, qu.front);
                DestroyQueue(qu);
                return true;
            }

            for (di = 0; di < 4; di++)
            {
                switch (di)
                {
                    case 0: i1 = i - 1; j1 = j; break;
                    case 1: i1 = i; j1 = j + 1; break;
                    case 2: i1 = i + 1; j1 = j; break;
                    case 3: i1 = i; j1 = j - 1; break;
                    default: i1 = i; j1 = j; break;
                }

                if (mg[i1, j1] == 0)
                {
                    e = new Box();
                    e.i = i1; e.j = j1;
                    e.pre = qu.front;
                    EnQueue(qu, e);
                    mg[i1, j1] = -1;
                }
            }
        }

        DestroyQueue(qu);
        return false;
    }

    static void Main()
    {
        int[,] init = {
            {1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,1,1,1,1,1,1},
            {1,0,0,0,1,1,1,1,1,1},
            {1,0,1,0,0,0,1,1,1,1},
            {1,0,1,0,1,0,0,1,1,1},
            {1,0,0,0,1,0,0,1,1,1},
            {1,1,1,0,1,0,0,0,0,1},
            {1,1,1,0,0,0,1,0,0,1},
            {1,1,1,1,1,1,1,1,1,1}
        };

        M = init.GetLength(0) - 2;
        N = init.GetLength(1) - 2;
        mg = new int[M + 2, N + 2];

        for (int i = 0; i < M + 2; i++)
            for (int j = 0; j < N + 2; j++)
                mg[i, j] = init[i, j];

        Console.WriteLine($"迷宫地图 ({M}行{N}列，1表示墙，0表示通道)：");
        for (int i = 0; i < M + 2; i++)
        {
            for (int j = 0; j < N + 2; j++)
                Console.Write(mg[i, j] + " ");
            Console.WriteLine();
        }
        Console.WriteLine();

        if (!MazePath(1, 1, M, N))
            Console.WriteLine("没有找到迷宫路径！");
    }
}

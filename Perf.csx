using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Console;

interface IExplicit
{
    void Sample();
    void Extra();
}

class Explicit : IExplicit
{
    void IExplicit.Sample()
    {
        var list1 = new List<int>();
        var list2 = new List<int>();

        for (int i = 0; i < 1000; i++)
        {
            list1.Add(i);
        }

        Task.Run(() => Parallel.ForEach(list1, item => { list2.Add(item); })).Wait();
    }

    void IExplicit.Extra()
    {
        for (int i = 0; i < 1000; i++) { }
    }
}

interface IImplicit
{
    void Sample();
    void Extra();
}

class Implicit : IImplicit
{
    public void Sample()
    {
        var list1 = new List<int>();
        var list2 = new List<int>();

        for (int i = 0; i < 1000; i++)
        {
            list1.Add(i);
        }

        Task.Run(() => Parallel.ForEach(list1, item => { list2.Add(item); })).Wait();
    }

    public void Extra()
    {
        for (int i = 0; i < 1000; i++) { }
    }
}

var stopWatch = new Stopwatch();
var times = 100000;

stopWatch.Start();
for (int i = 0; i < times; i++)
{
    var obj = new Explicit() as IExplicit;
    obj.Sample();
    obj.Extra();
}
stopWatch.Stop();
WriteLine($"Explicit: {stopWatch.Elapsed.TotalMilliseconds:n}");

stopWatch.Reset();

stopWatch.Start();
for (int i = 0; i < times; i++)
{
    ((IExplicit)new Explicit()).Sample();
    ((IExplicit)new Explicit()).Extra();
}
stopWatch.Stop();
WriteLine($"Explicit: {stopWatch.Elapsed.TotalMilliseconds:n}");

stopWatch.Reset();

stopWatch.Start();
for (int i = 0; i < times; i++)
{
    var obj = new Implicit() as IImplicit;
    obj.Sample();
    obj.Extra();
}
stopWatch.Stop();
WriteLine($"Implicit: {stopWatch.Elapsed.TotalMilliseconds:n}");

stopWatch.Reset();

stopWatch.Start();
for (int i = 0; i < times; i++)
{
    new Implicit().Sample();
    new Implicit().Extra();
}
stopWatch.Stop();
WriteLine($"Implicit: {stopWatch.Elapsed.TotalMilliseconds:n}");

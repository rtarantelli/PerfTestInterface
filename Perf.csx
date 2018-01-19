using System.Diagnostics;
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
        for (int i = 0; i < 1000; i++) { } 
    }

    void IExplicit.Extra() { }
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
        for (int i = 0; i < 1000; i++) { } 
    }

    public void Extra() { }
}

var stopWatch = new Stopwatch();
var times = 10 * 10 * 1000;

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
    ((IExplicit)new Explicit()).Sample();
    ((IExplicit)new Explicit()).Extra();
}
stopWatch.Stop();
WriteLine($"Explicit: {stopWatch.Elapsed.TotalMilliseconds:n}");

stopWatch.Reset();

stopWatch.Start();
for (int i = 0; i < times; i++)
{
    new Implicit().Sample();
    new Implicit().Extra();
}
stopWatch.Stop();
WriteLine($"Implicit: {stopWatch.Elapsed.TotalMilliseconds:n}");

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

/// <summary>
/// The ThreadExecutor is the concrete implementation of the IScheduler.
/// You can send any class to the judge system as long as it implements
/// the IScheduler interface. The Tests do not contain any <e>Reflection</e>!
/// </summary>
public class ThreadExecutor : IScheduler
{
    private List<Task> byInsertion;
    private Dictionary<int, Task> tasks = new Dictionary<int, Task>();

    public ThreadExecutor()
    {
        this.byInsertion = new List<Task>();
        this.tasks = new Dictionary<int, Task>();
    }

    public int Count => byInsertion.Count;

    public void ChangePriority(int id, Priority newPriority)
    {
        if (!this.tasks.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        this.tasks[id].TaskPriority = newPriority;
    }

    public bool Contains(Task task)
    {
        return this.tasks.ContainsKey(task.Id);
    }

    public int Cycle(int cycles)
    {
        if(this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        int completedTasks = 0;

        for (int i = 0; i < this.byInsertion.Count; i++)
        {
            this.byInsertion[i].DecreaseConsumption(cycles);

            if (this.byInsertion[i].Consumption <= 0)
            {
                this.tasks.Remove(this.byInsertion[i].Id);
                this.byInsertion.RemoveAt(i);
                completedTasks++;
                i--;
            }
        }

        return completedTasks;
    }

    public void Execute(Task task)
    {
        if (this.Contains(task))
        {
            throw new ArgumentException();
        }

        this.byInsertion.Add(task);
        this.tasks.Add(task.Id, task);
    }

    public IEnumerable<Task> GetByConsumptionRange(int lo, int hi, bool inclusive)
    {
        List<Task> tasks = null;

        if (inclusive)
        {
            tasks = this.byInsertion.Where(t => t.Consumption >= lo && t.Consumption <= hi).ToList();
        }
        else
        {
            tasks = this.byInsertion.Where(t => t.Consumption > lo && t.Consumption < hi).ToList();
        }

        return tasks.OrderBy(t => t.Consumption).ThenByDescending(t => t.TaskPriority).ToList();
    }

    public Task GetById(int id)
    {
        if (!this.tasks.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        return this.tasks[id];
    }

    public Task GetByIndex(int index)
    {
        if (this.byInsertion.Count <= index || index < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        return this.byInsertion[index];
    }

    public IEnumerable<Task> GetByPriority(Priority type)
    {
        return this.byInsertion
            .Where(t => t.TaskPriority == type)
            .OrderByDescending(t => t.Id)
            .ToList();
    }

    public IEnumerable<Task> GetByPriorityAndMinimumConsumption(Priority priority, int lo)
    {
        return this.byInsertion
            .Where(t => t.TaskPriority == priority && t.Consumption <= lo)
            .OrderByDescending(t => t.Id)
            .ToList();
    }


    public IEnumerator<Task> GetEnumerator()
    {
        return this.byInsertion.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
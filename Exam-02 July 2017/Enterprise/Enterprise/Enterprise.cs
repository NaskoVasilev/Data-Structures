using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enterprise : IEnterprise
{
    private Dictionary<Guid, Employee> byGuid;

    public Enterprise()
    {
        this.byGuid = new Dictionary<Guid, Employee>();
    }

    public int Count => this.byGuid.Count;

    public void Add(Employee employee)
    {
        this.byGuid.Add(employee.Id, employee);
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        return this.byGuid.Values.Where(x => x.Position == position && x.Salary >= minSalary);
    }

    public bool Change(Guid guid, Employee employee)
    {
        if (!this.byGuid.ContainsKey(guid))
        {
            return false;
        }

        this.byGuid[guid] = employee;
        return true;
    }

    public bool Contains(Guid guid)
    {
        return this.byGuid.ContainsKey(guid);
    }

    public bool Contains(Employee employee)
    {
        return this.Contains(employee.Id);
    }

    public bool Fire(Guid guid)
    {
        if (!this.byGuid.ContainsKey(guid))
        {
            return false;
        }

        this.byGuid.Remove(guid);
        return true;
    }

    public Employee GetByGuid(Guid guid)
    {
        if (!this.byGuid.ContainsKey(guid))
        {
            throw new ArgumentException();
        }

        return this.byGuid[guid];
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        IEnumerable<Employee> result = this.byGuid.Values.Where(x => x.Position == position);

        if (!result.Any())
        {
            throw new ArgumentException();
        }

        return result;
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        IEnumerable<Employee> result = this.byGuid.Values.Where(x => x.Salary >= minSalary);

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        IEnumerable<Employee> result = this.byGuid.Values.Where(x => x.Salary == salary && x.Position == position);

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public Position PositionByGuid(Guid guid)
    {
        if (!this.byGuid.ContainsKey(guid))
        {
            throw new InvalidOperationException();
        }

        return this.byGuid[guid].Position;
    }

    public bool RaiseSalary(int months, int percent)
    {
        bool result = false;
        double decimalPercent = percent / 100.0;

        foreach (var employeeByGuid in this.byGuid)
        {
            if (employeeByGuid.Value.HireDate.AddMonths(months) <= DateTime.Now)
            {
                employeeByGuid.Value.Salary += employeeByGuid.Value.Salary * decimalPercent;
                result = true;
            }
        }

        return result;
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        return this.byGuid.Values.Where(x => x.FirstName == firstName);
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        return this.byGuid.Values
            .Where(x => x.FirstName == firstName && x.LastName == lastName && x.Position == position);
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        return this.byGuid.Values.Where(x => positions.Contains(x.Position));
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        return this.byGuid.Values
           .Where(x => x.Salary >= minSalary && x.Salary <= maxSalary);
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        return this.byGuid.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
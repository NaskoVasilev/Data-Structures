using System;
using System.Collections.Generic;
using System.Linq;

public class Judge : IJudge
{
    private SortedSet<int> users;
    private SortedSet<int> contests;
    private Dictionary<int, Submission> submissions;

    public Judge()
    {
        this.users = new SortedSet<int>();
        this.contests = new SortedSet<int>(); ;
        this.submissions = new Dictionary<int, Submission>();
    }

    public void AddContest(int contestId)
    {
        this.contests.Add(contestId);
    }

    public void AddSubmission(Submission submission)
    {
        ValidateUserIdAndContestIdExist(submission.UserId, submission.ContestId);

        if (this.submissions.ContainsKey(submission.Id))
        {
            return;
        }

        this.submissions.Add(submission.Id, submission);
    }

    public void AddUser(int userId)
    {
        this.users.Add(userId);
    }

    public void DeleteSubmission(int submissionId)
    {
        if (!this.submissions.ContainsKey(submissionId))
        {
            throw new InvalidOperationException();
        }

        this.submissions.Remove(submissionId);
    }

    public IEnumerable<Submission> GetSubmissions()
    {
        return this.submissions.Values.OrderBy(x => x.Id);
    }

    public IEnumerable<int> GetUsers()
    {
        return this.users;
    }

    public IEnumerable<int> GetContests()
    {
        return this.contests;
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        return this.submissions.Values
            .Where(x => x.Points >= minPoints
            && x.Points <= maxPoints
            && x.Type == submissionType);
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {
        return this.submissions.Values
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Points)
            .ThenBy(x => x.Id)
            .Select(x => x.ContestId)
            .Distinct();
    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        ValidateUserIdAndContestIdExist(userId, contestId);

        var result = this.submissions.Values
            .Where(x => x.Points == points
            && x.ContestId == contestId
            && x.UserId == userId);

        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        return this.submissions.Values
            .Where(x => x.Type == submissionType)
            .Select(x => x.ContestId)
            .Distinct();
    }

    private void ValidateUserIdAndContestIdExist(int userId, int contestId)
    {
        if (!this.users.Contains(userId) || !this.contests.Contains(contestId))
        {
            throw new InvalidOperationException();
        }
    }
}
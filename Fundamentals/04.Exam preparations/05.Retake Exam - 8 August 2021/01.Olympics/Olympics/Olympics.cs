using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    private Dictionary<int, Competition> races = new Dictionary<int, Competition>();
    private Dictionary<int, Competitor> competitors = new Dictionary<int, Competitor>();

    public void AddCompetitor(int id, string name)
    {
        var comp = new Competitor(id, name);
        competitors.Add(id, comp);   
    }

    public void AddCompetition(int id, string name, int participantsLimit)
    {
        var race = new Competition(name, id, participantsLimit);
        races.Add(id, race);    
    }

    public void Compete(int competitorId, int competitionId)
    {
        if (!competitors.ContainsKey(competitorId) || !races.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        var race = races[competitionId];
        var participant = competitors[competitorId];

        race.IdsCompetitors.Add(competitorId, participant);
        participant.TotalScore += race.Score;
    }

    public int CompetitionsCount() => races.Count;

    public int CompetitorsCount() => competitors.Count;

    public bool Contains(int competitionId, Competitor comp)
    {
        if (races.ContainsKey(competitionId) == false 
           || competitors.ContainsKey(comp.Id) == false)
        {
            throw new ArgumentException();
        }

        return races[competitionId].IdsCompetitors.ContainsKey(comp.Id);
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        if (!competitors.ContainsKey(competitorId) || !races.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        var race = races[competitionId];
        var participant = competitors[competitorId];

        if (race.IdsCompetitors.Remove(participant.Id) == false) 
            throw new ArgumentException();

        participant.TotalScore -= race.Score;
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
        => competitors.Values
                    .Where(c => c.TotalScore > min && c.TotalScore <= max)
                    .OrderBy(c => c.Id);

    public IEnumerable<Competitor> GetByName(string name)
    {
        var result = competitors.Values.Where(c => c.Name == name)
                                    .OrderBy(c => c.Id);
        if (result.Count() == 0)
        {
            throw new ArgumentException();
        }

        return result;
    }

    public Competition GetCompetition(int id)
    {
        if (!races.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        return races[id];
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
        => competitors.Values
                    .Where(c => c.Name.Length >= min && c.Name.Length <= max)
                    .OrderBy(c => c.Id);
}
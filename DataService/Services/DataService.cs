using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services.Interfaces;

namespace rawdata_portfolioproject_2.Services
{
    public class DataService : IDataService
    {
        public Query CreateQuery(int profileId, DateTime timeSearched, string queryText)
        {
            using var db = new StackOverflowContext();
            Query query = new Query();
            query.ProfileId = profileId;
            query.TimeSearched = timeSearched;
            query.QueryText = queryText;
            db.Queries.Add(query);
            db.SaveChanges();
            return query;
        }

        public Query GetQuery(int profileId, DateTime timeSearched)
        {
            using var db = new StackOverflowContext();
            return db.Queries.Find(profileId, timeSearched);
        }

        public List<Query> GetAllQueries(int profileId)
        {
            using var db = new StackOverflowContext();
            return db.Queries.Where(x => x.ProfileId == profileId).Select(x => x).ToList();
        }

        public List<Query> GetQueriesBefore(int profileId, DateTime timeSearched)
        {
            using var db = new StackOverflowContext();
            return db.Queries.Where(x => x.TimeSearched.CompareTo(timeSearched) < 0).Select(x => x).ToList();
        }

        public List<Query> GetQueriesAfter(int profileId, DateTime timeSearched)
        {
            using var db = new StackOverflowContext();
            return db.Queries.Where(x => x.TimeSearched.CompareTo(timeSearched) > 0).Select(x => x).ToList();
        }

        public List<Query> GetQueriesByString(int profileId, params string[] keywords)
        {
            using var db = new StackOverflowContext();
            List<Query> queries = new List<Query>();
            foreach (var keyword in keywords) // smarter way to do this with lambda functions??
            {
                foreach (var query in db.Queries.ToList())
                {
                    if (query.QueryText.Contains(keyword))
                        queries.Add(query);
                }
            }
            return queries;
        }

        public bool DeleteQuery(int profileId, DateTime timeSearched)
        {
            using var db = new StackOverflowContext();
            Query query = db.Queries.Find(profileId, timeSearched);

            if (query == null)
                return false;

            db.Queries.Remove(query);
            db.SaveChanges();
            return true;
        }

        public bool DeleteQueries(List<Query> queries)
        {
            using var db = new StackOverflowContext();
            foreach (var query in queries)
            {
                db.Queries.Remove(query);
            }

            db.SaveChanges();
            return true; // make void or check for nulls
        }
    }
}
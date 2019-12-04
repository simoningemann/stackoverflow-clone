using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services.Interfaces;

namespace rawdata_portfolioproject_2.Services
{
    public class QueryService : IQueryService
    {
        public Query CreateQuery(int profileId, string queryText)
        {
            using var db = new StackOverflowContext();
            Query query = new Query();
            query.QueryId = NextQueryId(db);
            query.ProfileId = profileId;
            query.TimeSearched = DateTime.Now;
            query.QueryText = queryText;

            try
            {
                db.Queries.Add(query);
                db.SaveChanges();
                return query;
            }
            catch
            {
                return null;
            }
        }

        public Query GetQuery(int queryId)
        {
            using var db = new StackOverflowContext();
            return db.Queries.Find(queryId);
        }

        public List<Query> GetQueries(int profileId)
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

        public Query DeleteQuery(int queryId, int profileId)
        {
            using var db = new StackOverflowContext();
            var query = db.Queries.Find(queryId);

            if (query == null) return null;
            if (query.ProfileId != profileId) return null;

            db.Queries.Remove(query);
            db.SaveChanges();
            return query;
        }

        private int NextQueryId(StackOverflowContext db)
        {
            var id = 1;
            var ids = db.Queries.Select(x => x.QueryId);

            while (ids.Contains(id))
            {
                id = id + 1;
            }

            return id;
        }
    }
}
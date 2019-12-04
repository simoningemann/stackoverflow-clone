using System;
using System.Collections.Generic;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services.Interfaces
{
    public interface IQueryService
    {
        Query CreateQuery(int profileId, string queryText);
        Query GetQuery(int queryId);
        List<Query> GetQueries(int profileId);
        List<Query> GetQueriesBefore(int profileId, DateTime timeSearched);
        List<Query> GetQueriesAfter(int profileId, DateTime timeSearched);
        List<Query> GetQueriesByString(int profileId, params string[] keywords);
        Query DeleteQuery(int queryId, int profileId);
    }
}
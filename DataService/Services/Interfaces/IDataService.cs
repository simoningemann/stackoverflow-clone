using System;
using System.Collections.Generic;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services.Interfaces
{
    public interface IDataService
    {
        Query CreateQuery(int profileId, DateTime timeSearched, string queryText);
        Query GetQuery(int profileId, DateTime timeSearched);
        List<Query> GetAllQueries(int profileId);
        List<Query> GetQueriesBefore(int profileId, DateTime timeSearched);
        List<Query> GetQueriesAfter(int profileId, DateTime timeSearched);
        List<Query> GetQueriesByString(int profileId, params string[] keywords);
        bool DeleteQuery(int profileId, DateTime timeSearched);
        bool DeleteQueries(List<Query> queries);
    }
}
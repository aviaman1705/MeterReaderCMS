using MeterReaderCMS.Models.Entities;
using MeterReaderCMS.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Repositories.Implementation
{
    public class SearchRepository : ISearchRepository
    {
        private MeterReaderDB context = new MeterReaderDB();

        public List<SearchItem> GetSearchResult(string term)
        {
            var searchResult = context.Database.SqlQuery<SearchItem>("SP_GetSearchResult @Term = {0}", term).ToList();
            return searchResult;
        }
    }
}
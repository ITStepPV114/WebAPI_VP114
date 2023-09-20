﻿using Ardalis.Specification;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLogic.Specifications
{
    public static class MoviesSpec
    {
        public class OrderedAll : Specification<Movie>
        {
            public OrderedAll()
            {
                Query
                    .OrderBy(x => x.Title)
                    .Include(x=>x.Genres)
                    .ThenInclude(x=>x.Genre);
            }
        }

        public class ById : Specification<Movie>
        {
            public ById(int id)
            {
                Query
                     .Where(x => x.Id == id)
                    .Include(x => x.Genres)
                    .ThenInclude(x => x.Genre)
                  ;
            }
        }

    }
}

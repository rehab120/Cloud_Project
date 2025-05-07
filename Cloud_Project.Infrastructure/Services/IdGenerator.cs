using Cloud_Project.Application.Common.Enums;
using Cloud_Project.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Project.Infrastructure.Services
{
    public class IdGenerator : IIdGenerator
    {
        private readonly CloudDbContext _context;

        public IdGenerator(CloudDbContext context)
        {
            _context = context;
        }

        public string GenerateId<T>(ModelPrefix prefix) where T : class
        {
            string prefixStr = prefix.ToString().ToLower() + "-";
            int nextId = 1;

            var lastNumber = _context.Set<T>()
                .Select(e => EF.Property<string>(e, "Id"))
                .Where(id => id.StartsWith(prefixStr))
                .AsEnumerable()
                .Select(id =>
                {
                    int number;
                    return int.TryParse(id.Split('-').Last(), out number) ? number : 0;
                })
                .OrderByDescending(n => n)
                .FirstOrDefault();

            nextId = lastNumber + 1;

            return $"{prefixStr}{nextId}";
        }
    }
}

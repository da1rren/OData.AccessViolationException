using System;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using OData.AccessViolationException.Models;

namespace OData.AccessViolationException.OData
{
    public class PeopleController : ODataController
    {
        private readonly Context _context;

        public PeopleController(Context context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.People);
        }

        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            return Ok(_context.People.Find(key));
        }
    }
}

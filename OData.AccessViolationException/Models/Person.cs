using System;

namespace OData.AccessViolationException.Models
{
    public class Person
    {
        public Guid Id { get; set; }

        public int? TeamId { get; set; }

        public Person()
        {
            Id = Guid.NewGuid();
        }
    }
}

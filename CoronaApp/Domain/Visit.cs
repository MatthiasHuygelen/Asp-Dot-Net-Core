using System;

namespace CoronaApp.Domain
{
    public class Visit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Company Company { get; set; }
        public DateTime Date { get; set; }
    }
}

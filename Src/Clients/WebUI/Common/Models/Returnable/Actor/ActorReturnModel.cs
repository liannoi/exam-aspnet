using System;
using System.ComponentModel.DataAnnotations;

namespace Exam.Clients.WebUI.Common.Models.Returnable.Actor
{
    public class ActorReturnModel
    {
        public int ActorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime Birthday { get; set; } = DateTime.Now;
    }
}
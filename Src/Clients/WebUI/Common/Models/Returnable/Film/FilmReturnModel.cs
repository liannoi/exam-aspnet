using System;
using System.ComponentModel.DataAnnotations;

namespace Exam.Clients.WebUI.Common.Models.Returnable.Film
{
    public class FilmReturnModel
    {
        public int FilmId { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        public DateTime PublishYear { get; set; } = DateTime.Now;

        public string Description { get; set; }
    }
}
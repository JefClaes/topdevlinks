using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopDevLinks.Models.ViewModels
{
    public class ArchiveItemViewModel
    {
        public ArchiveItemViewModel(string id, DateTime date)
        {
            Id = id;
            Date = date;
        }

        public string Id { get; set; }

        public DateTime Date { get; set; }
    }
}
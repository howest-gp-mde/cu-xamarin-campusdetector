using System;
using System.Collections.Generic;
using System.Text;

namespace MDE.CampusDetector.Domain.Services.Api
{
    public class CampusDto
    {
        public int Id { get; set; }

        public double[] Coordinates { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Schools
{
    public class UpdateSchoolRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EstablishedOn { get; set; }
    }
}

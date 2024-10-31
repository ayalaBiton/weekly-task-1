﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weekly_task_1.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public eStatus Status { get; set; }
        public DateTime TargetDate { get; set; }
    }
}

﻿using QTF.Data.Models;
using QTF.Domain.Entity.AnswereBundle;
using QTF.Domain.Entity.QuestBundle;
using System.Collections.Generic;

namespace QTF.Data.ViewModels
{
    public class TaskViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public TaskType TaskType { get; set; }
        public ICollection<TaskAnswer> Answers { get; set; }
        public int TaskId { get; private set; }

        public TaskViewModel(QuestTask task)
        {
            this.Title = task.Title;
            this.Content = task.Content;
            this.TaskType = task.TaskType;
            this.Answers = task.Answers;
            this.TaskId = task.Id;
        }
    }
}
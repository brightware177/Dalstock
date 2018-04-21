﻿using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dalstock_WebApp_Mysql_identity_19_02.Models
{
    public class DebitViewModel
    {
        [Required(ErrorMessage = "Please select at least one option")]
        public Item SelectedItem;
        public List<Workplace> Workplaces { get; set; }
        public Workplace SelectedWorkplace { get; set; }
        public string SelectedWorkplaceId { get; set; }
        [Required]
        public virtual List<Item> Items { get; set; }
    }
    public class DepositViewModel
    {
        [Required(ErrorMessage = "Please select at least one option")]
        public Item SelectedItem;
        [Required]
        public virtual List<Item> Items { get; set; }
    }
    public class TableViewModel
    {
        public string ItemId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string WorkplaceId { get; set; }
    }
    public class DepositTableViewModel
    {
        public string ItemId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
    public class WorkplaceViewModel
    {
        public SelectList Cities { get; set; }
        public Workplace Workplace { get; set; }
    }
}
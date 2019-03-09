using Domain;
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
    public class BobbinViewModel
    {
        public List<CableType> CableTypes { get; set; }
        public List<Infrastructure> Infrastructures { get; set; }
        public Bobbin Bobbin { get; set; }
    }
    public class BobbinDetailViewModel
    {
        public BobbinDebit BobbinDebit { get; set; }
        public Bobbin Bobbin { get; set; }
        public List<SelectListItem> WorkplacesList { get; set; }
        public EmailViewModel EmailViewModel { get; set; }
    }
    public class ItemHistoryViewModel
    {
        public List<Debit> Debits { get; set; }
        public List<Deposit> Deposits { get; set; }
        public Workplace SelectedItem;
        public List<Workplace> Workplaces { get; set; }
    }
    public class DashboardViewModel
    {
        public Bobbin LatestBobbin { get; set; }
        public Workplace LatestWorkplace { get; set; }
        public int TotalAmountStock { get; set; }
        public decimal CablePerc { get; set; }
        public List<Item> InsufficientItems { get; set; }
        public int TotalAmountWorkplaces { get; set; }
    }
    public class WorkplaceDetailViewModel
    {
        public EmailViewModel EmailViewModel { get; set; }
        public Workplace Workplace { get; set; }
        public bool PrintItems { get; set; }
    }
    public class ItemsImportViewModel
    {
        public List<Deposit> ImportedItems { get; set; }
        public List<Deposit> FailedItems { get; set; }
    }
}
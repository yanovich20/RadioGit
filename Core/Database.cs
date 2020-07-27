using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class TEntity {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Deleted { get; set; }
    }
    public class ReclameBlock: TEntity
    { 
        [Display(Name="Название")]
        public string Name { get; set; }
        [Display(Name = "Код")]
        public string Code { get; set; }
        [Display(Name = "Период")]
        public Period Period { get; set; }
        [Display(Name = "Ответственный")]
        public Employer Responsible { get; set; }
        [Display(Name = "Ответственный")]
        public long ResponsibleId { get; set; }
        [Display(Name = "Коментарий")]
        public string Comment { get; set; }
        [Display(Name = "Активен")]
        public bool Status { get; set; }
        public List<Release> Releases { get; set; }
    }

    public class Release : TEntity
    {
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Display(Name = "Длительность")]
        public TimeSpan Duration { get; set; }
        [Display(Name = "Ведущий")]
        public Employer Leading { get; set; }
        [Display(Name = "Ведущий")]
        public long LeadingId { get; set; }
        [Display(Name = "Статус")]
        public State State { get; set; }
        [Display(Name = "Стоимость")]
        public decimal Cost { get; set; }
        public long ReclameBlockId { get; set; }
        public ReclameBlock ReclameBlock { get; set; }
    }

    public class Employer : TEntity
    {
        [Display(Name = "ФИО")]
        public string Name { get; set; }
    }

    public enum Period {
        [Display(Name = "Не установлен")]
        None =0,
        [Display(Name = "Час")]
        Hour =1,
        [Display(Name = "День")]
        Day =2,
        [Display(Name = "Неделя")]
        Week =3
    }

    public enum State {
        [Display(Name = "Не установлен")]
        None =0,
        [Display(Name = "Запланирован")]
        Planned =1,
        [Display(Name = "В эфире")]
        OnAir =2,
        [Display(Name = "Завершен")]
        Completed =3
    }
}

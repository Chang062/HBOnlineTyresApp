using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class NewSpecificationVM
    {
        public int TyreId { get; set; }
        [Display(Name = "Tyre Size")]
        public string Size { get; set; }
        [Display(Name = "Rim Size")]
        public string RimSize { get; set; }
        [Display(Name = "Speed Rating")]
        public string ServiceDescription { get; set; }
        [Display(Name = "Sidewall")]
        public string SideWall { get; set; }
        [Display(Name = "Diameter (in)")]
        public string Diameter { get; set; }
        [Display(Name = "Max PSI")]
        public string MaxPSI { get; set; }
        [Display(Name = "Section Width (in)")]
        public string SectionWidth { get; set; }
        [Display(Name = "Max Load (lb)")]
        public string MaxLoad { get; set; }
        [Display(Name = "Weight (lb)")]
        public string Weight { get; set; }
        [Display(Name = "Thread Dept (in)")]
        public string ThreadDept { get; set; }
        [Display(Name = "Approved Rim Width")]
        public string AprovedRimWidth { get; set; }
        [Display(Name = "Base Cost")]
        public double Cost { get; set; }
    }
}

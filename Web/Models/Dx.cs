using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuedesPasARG.Models
{
    public class Dx
    {
        public int id { get; set; }
        public int edad { get; set; }
        public int sistolica { get; set; }
        public int diastolica { get; set; }
        public double arp { get; set; }
        public double conc_aldo { get; set; }
        public double cromogranina_a { get; set; }
        public double dopamina { get; set; }
        public double epinefrina { get; set; }
        public double igf1 { get; set; }
        public double norepinefrina { get; set; }
        public double serotonina { get; set; }
        public double t4 { get; set; }
        public double tsh { get; set; }
        public double acido_5_hidrox { get; set; }
        public double acido_vainillin_man { get; set; }
        public double cortisol_libre { get; set; }
        public double epinefrina_orina { get; set; }
        public double norepinefrina_orina { get; set; }
        public double proteinuria { get; set; }
        public double sodio { get; set; }
        public double supr_dexa { get; set; }
        public double tfg { get; set; }
        public Boolean coartacion { get; set; }
        public Boolean estenosis_renal { get; set; }
        public int quistes { get; set; }
        public int iah { get; set; }
        public double cortisol_nocturno { get; set; }
        public Boolean ant_poliquistosis { get; set; }
        public Boolean cons_drogdas { get; set; }
        public Boolean cons_otras { get; set; }

        public Boolean cons_farmacos { get; set; }
        public string nivelPA { get; set; }
        public string etiologia { get; set; }
        public string estado { get; set; }
        public string comentario { get; set; }

    }
}

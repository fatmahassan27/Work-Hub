using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Entity
{
	public class City
	{
		[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
		public string Name{ get; set; }
		public List<District> Districtlist { get; set; }
    }
}

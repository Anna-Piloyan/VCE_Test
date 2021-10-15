using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibraryClass
{
	[Serializable]
	[XmlRoot(ElementName = "Answer")]
	public class Answer
	{
		[XmlElement(ElementName = "Description")]
		public string Description { get; set; }
		[XmlElement(ElementName = "IsRight")]
		public string IsRight { get; set; }

		public override string ToString()
		{
			return $"{Description}";
		}
	}
}

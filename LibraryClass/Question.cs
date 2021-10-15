using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibraryClass
{
	[Serializable]
	[XmlRoot(ElementName = "Question")]
	public class Question
	{
		[XmlElement(ElementName = "Description")]
		public string Description { get; set; }

		[XmlElement(ElementName = "Answer")]
		public List<Answer> Answer { get; set; }

		public Question()
		{
			Answer = new List<Answer>();
		}
		public override string ToString()
		{
			return $"{Description}";
		}
	}
}

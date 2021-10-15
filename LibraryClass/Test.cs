using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibraryClass
{
	[Serializable]

	[XmlRoot(ElementName = "Test")]
	public class Test
	{
		[XmlElement(ElementName = "Author")]
		public string Author { get; set; }
		[XmlElement(ElementName = "TestName")]
		public string TestName { get; set; }
		[XmlElement(ElementName = "QuestionCount")]
		public string QuestionCount { get; set; }
		[XmlElement(ElementName = "Difficulty")]
		public string Difficulty { get; set; }
		[XmlElement(ElementName = "Question")]
		public List<Question> Question { get; set; }
		public Test()
		{
			Question = new List<Question>();
		}
	}
}

namespace WebAdles.Models
{
    public class JSOX
    {
        public int RuleID { get; set; }
        public bool isEnabled { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public string RegexUpper { get; set; }
        public string RegexLower { get; set; }
        public string RegexNumeric { get; set; }
        public string RegexSpecial { get; set; }
    }
}
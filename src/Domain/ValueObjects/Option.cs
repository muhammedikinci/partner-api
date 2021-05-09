using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public class Option
    {
        public string OptionName { get; set; }
        public List<OptionValue> OptionValues { get; set; }
    }
}
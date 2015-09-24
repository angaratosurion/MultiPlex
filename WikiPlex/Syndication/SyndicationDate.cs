using System;

namespace WikiPlex.Syndication
{
    /// <summary>
    /// The syndication date, which is necessary in case the date cannot be parsed as a <see cref="DateTime"/>.
    /// </summary>
    public class SyndicationDate
    {
        /// <summary>
        /// Gets the raw value of date.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyndicationDate"/> class.
        /// </summary>
        /// <param name="value">The value of the date.</param>
        public SyndicationDate(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Overidden. Will attemp to convert the date and return its representation.
        /// </summary>
        /// <returns>The long date format of successfully parsed date, or the raw date value if it cannot be parsed.</returns>
        public override string ToString()
        {
            try
            {
                return DateTime.Parse(Value).ToLongDateString();
            }
            catch
            {
                return Value;
            }
        }
    }
}
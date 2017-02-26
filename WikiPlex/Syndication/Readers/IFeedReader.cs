namespace WikiPlex.Syndication
{
    /// <summary>
    /// Defines the <see cref="FeedReader"/> contract.
    /// </summary>
    public interface IFeedReader
    {
        /// <summary>
        /// Will read the feed.
        /// </summary>
        /// <returns>The read syndicated feed.</returns>
        SyndicationFeed Read();
    }
}
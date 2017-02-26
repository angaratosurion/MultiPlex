using System.Collections.Generic;
using WikiPlex.Web.Sample.Models;

namespace WikiPlex.Web.Sample.Repositories
{
    public interface IWikiRepository
    {
        Content Get(string slug, string title);
        Content Get(int id);
        Content GetByVersion(int id, int version);
        ICollection<Content> GetHistory(int titleId);
        int Save(int id, string slug, string title, string source);
    }
}
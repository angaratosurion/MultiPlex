using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MultiPlex.Web.Sample.Models;

namespace MultiPlex.Web.Sample.Repositories
{
    public class WikiRepository : IWikiRepository
    {
        private readonly string connectionString;

        public WikiRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["WikiConnectionString"].ConnectionString;
        }

        public Content Get(string slug, string title)
        {
            const string sql = @"SELECT TOP 1
                                    C.Id, C.Source, C.Version, C.VersionDate, C.TitleId, T.Name, T.Slug,
                                    (SELECT COUNT(*) FROM Content WHERE TitleId = T.Id)
                                 FROM Content C
                                 JOIN Title T ON T.Id = C.TitleId
                                 WHERE T.Slug = @Slug
                                    OR T.Name = @Title
                                 ORDER BY C.Version DESC";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Slug", slug));
                cmd.Parameters.Add(new SqlParameter("@Title", title));

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                        return BuildContent(reader);
                }
            }

            return null;
        }

        public Content Get(int id)
        {
            const string sql = @"SELECT TOP 1
                                    C.Id, C.Source, C.Version, C.VersionDate, C.TitleId, T.Name, T.Slug,
                                    (SELECT COUNT(*) FROM Content WHERE TitleId = T.Id)
                                 FROM Content C
                                 JOIN Title T ON T.Id = C.TitleId
                                 WHERE T.Id = @Id
                                 ORDER BY C.Version DESC";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id", id));

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                        return BuildContent(reader);
                }
            }

            return null;
        }

        public Content GetByVersion(int id, int version)
        {
            const string sql = @"SELECT TOP 1
                                    C.Id, C.Source, C.Version, C.VersionDate, C.TitleId, T.Name, T.Slug,
                                    (SELECT COUNT(*) FROM Content WHERE TitleId = T.Id)
                                 FROM Content C
                                 JOIN Title T ON T.Id = C.TitleId
                                 WHERE T.Id = @Id
                                 AND C.Version = @Version";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.Parameters.Add(new SqlParameter("@Version", version));

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                        return BuildContent(reader);
                }
            }

            return null;
        }

        public ICollection<Content> GetHistory(int titleId)
        {
            const string sql = @"SELECT
                                    C.Id, C.Source, C.Version, C.VersionDate, C.TitleId, T.Name, T.Slug, 0
                                 FROM Content C
                                 JOIN Title T ON T.Id = C.TitleId
                                 WHERE T.Id = @Id
                                 ORDER BY C.Version DESC";

            var history = new List<Content>();

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@Id", titleId));

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                        history.Add(BuildContent(reader));
                }
            }

            return history;
        }

        public int Save(int id, string slug, string title, string source)
        {
            const string sql = @"DECLARE @ContentCount INT

                                 SELECT @ContentCount = (SELECT COUNT(*) FROM Content WHERE TitleId = T.Id) 
                                 FROM Title T
                                 WHERE T.Id = @TitleId

                                 IF (@TitleId = 0) BEGIN
                                    INSERT INTO Title (Name, Slug)
                                    VALUES (@Name, @Slug)

                                    SELECT @TitleId = SCOPE_IDENTITY()
                                 END

                                 INSERT INTO Content (TitleId, Source, Version, VersionDate)
                                 VALUES (@TitleId, @Source, ISNULL(@ContentCount, 0) + 1, GETDATE())

                                 SELECT @TitleId";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@TitleId", id));
                cmd.Parameters.Add(new SqlParameter("@Slug", slug));
                cmd.Parameters.Add(new SqlParameter("@Name", title));
                cmd.Parameters.Add(new SqlParameter("@Source", source));

                conn.Open();
                return (int) cmd.ExecuteScalar();
            }
        }

        private static Content BuildContent(IDataRecord reader)
        {
            return new Content
                       {
                           Id = reader.GetInt32(0),
                           Source = reader.GetString(1),
                           Version = reader.GetInt32(2),
                           VersionDate = reader.GetDateTime(3),
                           Title = new Title
                                       {
                                           Id = reader.GetInt32(4),
                                           Name = reader.GetString(5),
                                           Slug = reader.GetString(6),
                                           MaxVersion = reader.GetInt32(7)
                                       }
                       };
        }
    }
}
using System;
using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.DAL
{
    public class FilmRepository : IFilmRepository
    {
        public void Insert(IUnitOfWork uow, FilmBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.FilmInsert))
            {
                cmd.Parameters.AddWithValue("@Title", String.IsNullOrEmpty(entity.Title) ? "" : entity.Title);
                cmd.Parameters.AddWithValue("@Subtitle", String.IsNullOrEmpty(entity.Subtitle) ? "" : entity.Subtitle);
                cmd.Parameters.AddWithValue("@FormatId", entity.FormatId);
                cmd.Parameters.AddWithValue("@GenreId01", entity.GenreId01);
                cmd.Parameters.AddWithValue("@GenreId02", entity.GenreId02);
                cmd.Parameters.AddWithValue("@Synopsis", String.IsNullOrEmpty(entity.Synopsis) ? "" : entity.Synopsis);
                cmd.Parameters.AddWithValue("@DurationMin", entity.DurationMin);
                cmd.Parameters.AddWithValue("@DownloadDate", entity.DownloadDate);
                cmd.Parameters.AddWithValue("@PremiereDate", entity.PremiereDate);
                cmd.Parameters.AddWithValue("@TrailerURL", String.IsNullOrEmpty(entity.TrailerURL) ? "" : entity.TrailerURL);
                cmd.Parameters.AddWithValue("@Note", entity.Note);
                cmd.Parameters.AddWithValue("@Cover", entity.Cover);
                cmd.Parameters.AddWithValue("@Background", entity.Background);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(IUnitOfWork uow, FilmBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.FilmUpdate))
            {
                cmd.Parameters.AddWithValue("@Title", entity.Title);
                cmd.Parameters.AddWithValue("@Subtitle", entity.Subtitle);
                cmd.Parameters.AddWithValue("@FormatId", entity.FormatId);
                cmd.Parameters.AddWithValue("@GenreId01", entity.GenreId01);
                cmd.Parameters.AddWithValue("@GenreId02", entity.GenreId02);
                cmd.Parameters.AddWithValue("@Synopsis", entity.Synopsis);
                cmd.Parameters.AddWithValue("@DurationMin", entity.DurationMin);
                cmd.Parameters.AddWithValue("@DownloadDate", entity.DownloadDate);
                cmd.Parameters.AddWithValue("@PremiereDate", entity.PremiereDate);
                cmd.Parameters.AddWithValue("@TrailerURL", entity.TrailerURL);
                cmd.Parameters.AddWithValue("@Note", entity.Note);
                cmd.Parameters.AddWithValue("@Cover", entity.Cover);
                cmd.Parameters.AddWithValue("@Background", entity.Background);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(IUnitOfWork uow, int pk)
        {
            using (var cmd = uow.CreateCommand(Queries.FilmDelete))
            {
                cmd.Parameters.AddWithValue("@Id", pk);
                cmd.ExecuteNonQuery();
            }
        }

        public FilmBO Single(IUnitOfWork uow, int pk)
        {
            using (var cmd = uow.CreateCommand(Queries.FilmSelectById))
            {
                cmd.Parameters.AddWithValue("@Id", pk);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                        return null;

                    return new FilmBO
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Title = rdr["Title"].ToString(),
                        Subtitle = rdr["Subtitle"].ToString(),
                        FormatId = Convert.ToInt32(rdr["FormatId"]),
                        GenreId01 = Convert.ToInt32(rdr["GenreId01"]),
                        GenreId02 = Convert.ToInt32(rdr["GenreId02"]),
                        Synopsis = rdr["Synopsis"].ToString(),
                        DurationMin = Convert.ToInt32(rdr["DurationMin"]),
                        DownloadDate = rdr["DownloadDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(rdr["DownloadDate"]),
                        PremiereDate = rdr["PremiereDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(rdr["PremiereDate"]),
                        TrailerURL = rdr["TrailerURL"].ToString(),
                        Note = Convert.ToDecimal(rdr["Note"]),
                        Cover = rdr["Cover"] == DBNull.Value ? null : (byte[])rdr["Cover"],
                        Background = rdr["Background"] == DBNull.Value ? null : (byte[])rdr["Background"],
                    };
                }
            }
        }

        public List<FilmBO> FindAll(IUnitOfWork uow)
        {
            var result = new List<FilmBO>();

            using (var cmd = uow.CreateCommand(Queries.FilmFindAll))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(new FilmBO
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Title = rdr["Title"].ToString(),
                            Subtitle = rdr["Subtitle"].ToString(),
                            FormatId = Convert.ToInt32(rdr["FormatId"]),
                            GenreId01 = Convert.ToInt32(rdr["GenreId01"]),
                            GenreId02 = Convert.ToInt32(rdr["GenreId02"]),
                            Synopsis = rdr["Synopsis"].ToString(),
                            DurationMin = Convert.ToInt32(rdr["DurationMin"]),
                            DownloadDate =
                                rdr["DownloadDate"] == DBNull.Value
                                    ? DateTime.MinValue
                                    : Convert.ToDateTime(rdr["DownloadDate"]),
                            PremiereDate =
                                rdr["PremiereDate"] == DBNull.Value
                                    ? DateTime.MinValue
                                    : Convert.ToDateTime(rdr["PremiereDate"]),
                            TrailerURL = rdr["TrailerURL"].ToString(),
                            Note = Convert.ToDecimal(rdr["Note"]),
                            Cover = rdr["Cover"] == DBNull.Value ? null : (byte[])rdr["Cover"],
                            Background = rdr["Background"] == DBNull.Value ? null : (byte[])rdr["Background"],
                        });
                    }
                }
            }
            return result;
        }

        public int MaxPK(IUnitOfWork uow)
        {
            using (var cmd = uow.CreateCommand(Queries.FilmMaxPk))
                return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
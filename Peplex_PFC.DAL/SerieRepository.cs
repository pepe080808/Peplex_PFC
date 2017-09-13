using System;
using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.DAL
{
    public class SerieRepository : ISerieRepository
    {
        public int Insert(IUnitOfWork uow, SerieBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.SerieInsert))
            {
                cmd.Parameters.AddWithValue("@Title", String.IsNullOrEmpty(entity.Title) ? "" : entity.Title);
                cmd.Parameters.AddWithValue("@GenreId01", entity.GenreId01);
                cmd.Parameters.AddWithValue("@GenreId02", entity.GenreId02);
                cmd.Parameters.AddWithValue("@Synopsis", String.IsNullOrEmpty(entity.Synopsis) ? "" : entity.Synopsis);
                cmd.Parameters.AddWithValue("@DurationMin", entity.DurationMin);
                cmd.Parameters.AddWithValue("@DownloadDate", entity.DownloadDate);
                cmd.Parameters.AddWithValue("@PremiereDate", entity.PremiereDate);
                cmd.Parameters.AddWithValue("@Note", entity.Note);
                cmd.Parameters.AddWithValue("@Cover", entity.Cover);
                cmd.Parameters.AddWithValue("@Background", entity.Background);
                cmd.Parameters.AddWithValue("@CoverOpt", entity.CoverOpt);
                cmd.Parameters.AddWithValue("@BackgroundOpt", entity.BackgroundOpt);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int Update(IUnitOfWork uow, SerieBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.SerieUpdate))
            {
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Title", entity.Title);
                cmd.Parameters.AddWithValue("@GenreId01", entity.GenreId01);
                cmd.Parameters.AddWithValue("@GenreId02", entity.GenreId02);
                cmd.Parameters.AddWithValue("@Synopsis", entity.Synopsis);
                cmd.Parameters.AddWithValue("@DurationMin", entity.DurationMin);
                cmd.Parameters.AddWithValue("@DownloadDate", entity.DownloadDate);
                cmd.Parameters.AddWithValue("@PremiereDate", entity.PremiereDate);
                cmd.Parameters.AddWithValue("@Note", entity.Note);
                cmd.Parameters.AddWithValue("@Cover", entity.Cover);
                cmd.Parameters.AddWithValue("@Background", entity.Background);
                cmd.Parameters.AddWithValue("@CoverOpt", entity.CoverOpt);
                cmd.Parameters.AddWithValue("@BackgroundOpt", entity.BackgroundOpt);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Delete(IUnitOfWork uow, int pk)
        {
            using (var cmd = uow.CreateCommand(Queries.SerieDelete))
            {
                cmd.Parameters.AddWithValue("@Id", pk);
                cmd.ExecuteNonQuery();
            }
        }

        public SerieBO Single(IUnitOfWork uow, int pk)
        {
            using (var cmd = uow.CreateCommand(Queries.SerieSelectById))
            {
                cmd.Parameters.AddWithValue("@Id", pk);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                        return null;

                    return new SerieBO
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Title = rdr["Title"].ToString(),
                        GenreId01 = Convert.ToInt32(rdr["GenreId01"]),
                        GenreId02 = Convert.ToInt32(rdr["GenreId02"]),
                        Synopsis = rdr["Synopsis"].ToString(),
                        DurationMin = Convert.ToInt32(rdr["DurationMin"]),
                        DownloadDate = rdr["DownloadDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(rdr["DownloadDate"]),
                        PremiereDate = rdr["PremiereDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(rdr["PremiereDate"]),
                        Note = Convert.ToDecimal(rdr["Note"]),
                        Cover = rdr["Cover"] == DBNull.Value ? null : (byte[])rdr["Cover"],
                        Background = rdr["Background"] == DBNull.Value ? null : (byte[])rdr["Background"],
                        CoverOpt = rdr["CoverOpt"] == DBNull.Value ? null : (byte[])rdr["CoverOpt"],
                        BackgroundOpt = rdr["BackgroundOpt"] == DBNull.Value ? null : (byte[])rdr["BackgroundOpt"],
                    };
                }
            }
        }

        public List<SerieBO> FindAll(IUnitOfWork uow)
        {
            var result = new List<SerieBO>();

            using (var cmd = uow.CreateCommand(Queries.SerieFindAll))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(new SerieBO
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Title = rdr["Title"].ToString(),
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
                            Note = Convert.ToDecimal(rdr["Note"]),
                            Cover = rdr["Cover"] == DBNull.Value ? null : (byte[])rdr["Cover"],
                            Background = rdr["Background"] == DBNull.Value ? null : (byte[])rdr["Background"],
                            CoverOpt = rdr["CoverOpt"] == DBNull.Value ? null : (byte[])rdr["CoverOpt"],
                            BackgroundOpt = rdr["BackgroundOpt"] == DBNull.Value ? null : (byte[])rdr["BackgroundOpt"],
                        });
                    }
                }
            }
            return result;
        }

        public int MaxPK(IUnitOfWork uow)
        {
            using (var cmd = uow.CreateCommand(Queries.SerieMaxPk))
                return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
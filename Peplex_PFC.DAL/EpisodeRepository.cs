using System;
using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.DAL
{
    public class EpisodeRepository : IEpisodeRepository
    {
        public void Insert(IUnitOfWork uow, EpisodeBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.EpisodeInsert))
            {
                cmd.Parameters.AddWithValue("@SerieId", entity.SerieId);
                cmd.Parameters.AddWithValue("@Season", entity.Season);
                cmd.Parameters.AddWithValue("@Number", entity.Number);
                cmd.Parameters.AddWithValue("@Name", String.IsNullOrEmpty(entity.Name) ? "" : entity.Name);
                cmd.Parameters.AddWithValue("@FormatId", entity.FormatId);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(IUnitOfWork uow, EpisodeBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.EpisodeUpdate))
            {
                cmd.Parameters.AddWithValue("@SerieId", entity.SerieId);
                cmd.Parameters.AddWithValue("@Season", entity.Season);
                cmd.Parameters.AddWithValue("@Number", entity.Number);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@FormatId", entity.FormatId);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(IUnitOfWork uow, int serieId, int season, int number)
        {
            using (var cmd = uow.CreateCommand(Queries.EpisodeDelete))
            {
                cmd.Parameters.AddWithValue("@SerieId", serieId);
                cmd.Parameters.AddWithValue("@Season", season);
                cmd.Parameters.AddWithValue("@Number", number);
                cmd.ExecuteNonQuery();
            }
        }

        public EpisodeBO Single(IUnitOfWork uow, int serieId, int season, int number)
        {
            using (var cmd = uow.CreateCommand(Queries.EpisodeSelectById))
            {
                cmd.Parameters.AddWithValue("@SerieId", serieId);
                cmd.Parameters.AddWithValue("@Season", season);
                cmd.Parameters.AddWithValue("@Number", number);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                        return null;

                    return new EpisodeBO
                    {
                        SerieId = Convert.ToInt32(rdr["SerieId"]),
                        Season = Convert.ToInt32(rdr["Season"]),
                        Number = Convert.ToInt32(rdr["Number"]),
                        Name = rdr["Name"].ToString(),
                        FormatId = Convert.ToInt32(rdr["FormatId"])
                    };
                }
            }
        }

        public List<EpisodeBO> FindAll(IUnitOfWork uow)
        {
            var result = new List<EpisodeBO>();

            using (var cmd = uow.CreateCommand(Queries.EpisodeFindAll))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(new EpisodeBO
                        {
                            SerieId = Convert.ToInt32(rdr["SerieId"]),
                            Season = Convert.ToInt32(rdr["Season"]),
                            Number = Convert.ToInt32(rdr["Number"]),
                            Name = rdr["Name"].ToString(),
                            FormatId = Convert.ToInt32(rdr["FormatId"])
                        });
                    }
                }
            }
            return result;
        }

        public int MaxPK(IUnitOfWork uow)
        {
            return -1;
        }
    }
}
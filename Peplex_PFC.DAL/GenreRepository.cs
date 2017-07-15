using System;
using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.DAL
{
    public class GenreRepository : IGenreRepository
    {
        public void Insert(IUnitOfWork uow, GenreBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.GenreInsert))
            {
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(IUnitOfWork uow, GenreBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.GenreUpdate))
            {
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(IUnitOfWork uow, int pk)
        {
            using (var cmd = uow.CreateCommand(Queries.GenreDelete))
            {
                cmd.Parameters.AddWithValue("@Id", pk);
                cmd.ExecuteNonQuery();
            }
        }

        public GenreBO Single(IUnitOfWork uow, int pk)
        {
            using (var cmd = uow.CreateCommand(Queries.GenreSelectById))
            {
                cmd.Parameters.AddWithValue("@Id", pk);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                        return null;

                    return new GenreBO
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                    };
                }
            }
        }

        public List<GenreBO> FindAll(IUnitOfWork uow)
        {
            var result = new List<GenreBO>();

            using (var cmd = uow.CreateCommand(Queries.GenreFindAll))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(new GenreBO
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Name = rdr["Name"].ToString(),
                        });
                    }
                }
            }
            return result;
        }

        public int MaxPK(IUnitOfWork uow)
        {
            using (var cmd = uow.CreateCommand(Queries.GenreMaxPk))
                return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}

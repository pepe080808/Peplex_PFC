using System;
using System.Collections.Generic;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.DAL
{
    public class FormatRepository : IFormatRepository
    {
        public void Insert(IUnitOfWork uow, FormatBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.FormatInsert))
            {
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@Quality", entity.Quality);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(IUnitOfWork uow, FormatBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.FormatUpdate))
            {
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@Quality", entity.Quality);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(IUnitOfWork uow, int pk)
        {
            using (var cmd = uow.CreateCommand(Queries.FormatDelete))
            {
                cmd.Parameters.AddWithValue("@Id", pk);
                cmd.ExecuteNonQuery();
            }
        }

        public FormatBO Single(IUnitOfWork uow, int pk)
        {
            using (var cmd = uow.CreateCommand(Queries.FormatSelectById))
            {
                cmd.Parameters.AddWithValue("@Id", pk);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                        return null;

                    return new FormatBO
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        Name = rdr["Name"].ToString(),
                        Quality = rdr["Quality"].ToString()
                    };
                }
            }
        }

        public List<FormatBO> FindAll(IUnitOfWork uow)
        {
            var result = new List<FormatBO>();

            using (var cmd = uow.CreateCommand(Queries.FormatFindAll))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(new FormatBO
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Name = rdr["Name"].ToString(),
                            Quality = rdr["Quality"].ToString(),
                        });
                    }
                }
            }
            return result;
        }

        public int MaxPK(IUnitOfWork uow)
        {
            using (var cmd = uow.CreateCommand(Queries.FormatMaxPk))
                return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
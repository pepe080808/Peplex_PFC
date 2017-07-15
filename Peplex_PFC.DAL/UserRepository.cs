using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Media;
using System.Xml;
using Peplex_PFC.BLL.InterfacesClasses.Classes.BO;
using Peplex_PFC.BLL.InterfacesClasses.Interfaces;

namespace Peplex_PFC.DAL
{
    public class UserRepository : IUserRepository
    {
        public void Insert(IUnitOfWork uow, UserBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.UserInsert))
            {
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@Email", entity.Email);
                cmd.Parameters.AddWithValue("@NickName", entity.NickName);
                cmd.Parameters.AddWithValue("@Password", entity.Password);
                cmd.Parameters.AddWithValue("@Bitmap", entity.Photo);
                cmd.Parameters.AddWithValue("@Permissions", entity.Permissions);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(IUnitOfWork uow, UserBO entity)
        {
            using (var cmd = uow.CreateCommand(Queries.UserUpdate))
            {
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Name", entity.Name);
                cmd.Parameters.AddWithValue("@Email", entity.Email);
                cmd.Parameters.AddWithValue("@NickName", entity.NickName);
                cmd.Parameters.AddWithValue("@Password", entity.Password);
                cmd.Parameters.AddWithValue("@Bitmap", entity.Photo);
                cmd.Parameters.AddWithValue("@Permissions", entity.Permissions);
                cmd.ExecuteNonQuery();
            }

            UpdateFilmSeen(uow, entity);
            UpdateSerieSeen(uow, entity);
            UpdateEpisodeSeen(uow, entity);
            UpdateFilmTime(uow, entity);
            UpdateEpisodeTime(uow, entity);
        }

        public void Delete(IUnitOfWork uow, int pk)
        {
            using (var cmd = uow.CreateCommand(Queries.UserDelete))
            {
                cmd.Parameters.AddWithValue("@Id", pk);
                cmd.ExecuteNonQuery();
            }
        }

        public UserBO Single(IUnitOfWork uow, int pk)
        {
            UserBO user = null;

            using (var cmd = uow.CreateCommand(Queries.UserSelectById))
            {
                cmd.Parameters.AddWithValue("@Id", pk);

                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        user = new UserBO
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Name = rdr["Name"].ToString(),
                            Email = rdr["Email"].ToString(),
                            NickName = rdr["NickName"].ToString(),
                            Password = rdr["Password"].ToString(),
                            Photo = rdr["Bitmap"] == DBNull.Value ? null : (byte[]) rdr["Bitmap"],
                            Permissions = Convert.ToInt32(rdr["Permissions"])
                        };
                    }
                }
            }

            if (user != null)
            {
                GetFilmSeen(uow, user);
                GetSerieSeen(uow, user);
                GetEpisodeSeen(uow, user);
                GetFilmTime(uow, user);
                GetEpisodeTime(uow, user);
            }

            return user;
        }

        public List<UserBO> FindAll(IUnitOfWork uow)
        {
            var result = new List<UserBO>();

            using (var cmd = uow.CreateCommand(Queries.UserFindAll))
            {
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        result.Add(new UserBO
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            Name = rdr["Name"].ToString(),
                            Email = rdr["Email"].ToString(),
                            NickName = rdr["NickName"].ToString(),
                            Password = rdr["Password"].ToString(),
                            Photo = rdr["Bitmap"] == DBNull.Value ? null : (byte[])rdr["Bitmap"],
                            Permissions = Convert.ToInt32(rdr["Permissions"])
                        });
                    }
                }
            }
            return result;
        }

        public int MaxPK(IUnitOfWork uow)
        {
            using (var cmd = uow.CreateCommand(Queries.UserMaxPk))
                return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private void GetFilmSeen(IUnitOfWork uow, UserBO user)
        {
            using (var cmd = uow.CreateCommand(Queries.UserGetFilmSeen))
            {
                cmd.Parameters.AddWithValue("@Id", user.Id);

                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        user.FilmSeen.Add(Convert.ToInt32(rdr["FilmId"]));
            }
        }

        private void GetSerieSeen(IUnitOfWork uow, UserBO user)
        {
            using (var cmd = uow.CreateCommand(Queries.UserGetSerieSeen))
            {
                cmd.Parameters.AddWithValue("@Id", user.Id);

                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        user.SerieSeen.Add(Convert.ToInt32(rdr["SerieId"]));
            }
        }

        private void GetEpisodeSeen(IUnitOfWork uow, UserBO user)
        {
            using (var cmd = uow.CreateCommand(Queries.UserGetEpisodeSeen))
            {
                cmd.Parameters.AddWithValue("@Id", user.Id);

                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        user.EpisodeSeen.Add(String.Format("{0};{1};{2}", rdr["SerieId"], rdr["Season"], rdr["Number"]));
            }
        }

        private void GetFilmTime(IUnitOfWork uow, UserBO user)
        {
            using (var cmd = uow.CreateCommand(Queries.UserGetFilmTime))
            {
                cmd.Parameters.AddWithValue("@Id", user.Id);

                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        user.FilmTime.Add(Convert.ToInt32(rdr["FilmId"]), Convert.ToInt32(rdr["SecondTime"]));
            }
        }

        private void GetEpisodeTime(IUnitOfWork uow, UserBO user)
        {
            using (var cmd = uow.CreateCommand(Queries.UserGetEpisodeTime))
            {
                cmd.Parameters.AddWithValue("@Id", user.Id);

                using (var rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        user.EpisodeTime.Add(String.Format("{0};{1};{2}", rdr["SerieId"], rdr["Season"], rdr["Number"]), Convert.ToInt32(rdr["SecondTime"]));
            }
        }

        private void UpdateFilmSeen(IUnitOfWork uow, UserBO user)
        {
            using (var cmd = uow.CreateCommand(Queries.UserUpdateFilmSeen))
            {
                cmd.Parameters.Add("@FilmId", SqlDbType.Int);
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@UserId", user.Id);

                foreach (var i in user.FilmSeen)
                {
                    cmd.Parameters["@FilmId"].Value = i;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateSerieSeen(IUnitOfWork uow, UserBO user)
        {
            using (var cmd = uow.CreateCommand(Queries.UserUpdateSerieSeen))
            {
                cmd.Parameters.Add("@SerieId", SqlDbType.Int);
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@UserId", user.Id);

                foreach (var i in user.SerieSeen)
                {
                    cmd.Parameters["@SerieId"].Value = i;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateEpisodeSeen(IUnitOfWork uow, UserBO user)
        {
            using (var cmd = uow.CreateCommand(Queries.UserUpdateEpisodeSeen))
            {
                cmd.Parameters.Add("@SerieId", SqlDbType.Int);
                cmd.Parameters.Add("@Season", SqlDbType.Int);
                cmd.Parameters.Add("@Number", SqlDbType.Int);
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@UserId", user.Id);

                foreach (var i in user.EpisodeSeen)
                {
                    var dataSplit = i.Split(';');
                    cmd.Parameters["@SerieId"].Value = dataSplit[0];
                    cmd.Parameters["@Season"].Value = dataSplit[1];
                    cmd.Parameters["@Number"].Value = dataSplit[2];
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateFilmTime(IUnitOfWork uow, UserBO user)
        {
            using (var cmd = uow.CreateCommand(Queries.UserUpdateFilmTime))
            {
                cmd.Parameters.Add("@FilmId", SqlDbType.Int);
                cmd.Parameters.Add("@SecondTime", SqlDbType.Int);
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@UserId", user.Id);

                foreach (var i in user.FilmTime.Keys)
                {
                    cmd.Parameters["@FilmId"].Value = i;
                    cmd.Parameters["@SecondTime"].Value = user.FilmTime[i];
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateEpisodeTime(IUnitOfWork uow, UserBO user)
        {
            using (var cmd = uow.CreateCommand(Queries.UserUpdateEpisodeTime))
            {
                cmd.Parameters.Add("@SerieId", SqlDbType.Int);
                cmd.Parameters.Add("@Season", SqlDbType.Int);
                cmd.Parameters.Add("@Number", SqlDbType.Int);
                cmd.Parameters.Add("@SecondTime", SqlDbType.Int);
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@UserId", user.Id);

                foreach (var i in user.EpisodeTime.Keys)
                {
                    var dataSplit = i.Split(';');
                    cmd.Parameters["@SerieId"].Value = dataSplit[0];
                    cmd.Parameters["@Season"].Value = dataSplit[1];
                    cmd.Parameters["@Number"].Value = dataSplit[2];
                    cmd.Parameters["@SecondTime"].Value = user.EpisodeTime[i];
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
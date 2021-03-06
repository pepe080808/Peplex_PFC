﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Peplex_PFC.DAL {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Queries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Queries() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Peplex_PFC.DAL.Queries", typeof(Queries).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE Episode WHERE SerieId = @SerieId AND Season = @Season AND Number = @Number
        ///.
        /// </summary>
        internal static string EpisodeDelete {
            get {
                return ResourceManager.GetString("EpisodeDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM Episode.
        /// </summary>
        internal static string EpisodeFindAll {
            get {
                return ResourceManager.GetString("EpisodeFindAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Episode (SerieId, Season, Number, Name, FormatId)
        ///VALUES (@SerieId, @Season, @Number, @Name, @FormatId);
        ///SELECT @@IDENTITY;.
        /// </summary>
        internal static string EpisodeInsert {
            get {
                return ResourceManager.GetString("EpisodeInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string EpisodeMaxPk {
            get {
                return ResourceManager.GetString("EpisodeMaxPk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT *
        ///FROM Episode
        ///WHERE SerieId = @SerieId AND Season = @Season AND Number = @Number.
        /// </summary>
        internal static string EpisodeSelectById {
            get {
                return ResourceManager.GetString("EpisodeSelectById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Episode
        ///SET Name = @Name, FormatId = @FormatId
        ///WHERE SerieId = @SerieId AND Season = @Season AND Number = @Number.
        /// </summary>
        internal static string EpisodeUpdate {
            get {
                return ResourceManager.GetString("EpisodeUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE Film WHERE Id = @Id.
        /// </summary>
        internal static string FilmDelete {
            get {
                return ResourceManager.GetString("FilmDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM Film.
        /// </summary>
        internal static string FilmFindAll {
            get {
                return ResourceManager.GetString("FilmFindAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Film (Title, Subtitle, FormatId, GenreId01, GenreId02, Synopsis, DurationMin, DownloadDate, PremiereDate, TrailerURL, Note, Cover, Background)
        ///VALUES(@Title, @Subtitle, @FormatId, @GenreId01, @GenreId02, @Synopsis, @DurationMin, @DownloadDate, @PremiereDate, @TrailerURL, @Note, @Cover, @Background);
        ///SELECT @@IDENTITY;.
        /// </summary>
        internal static string FilmInsert {
            get {
                return ResourceManager.GetString("FilmInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ISNULL(MAX(Id), 0) FROM Film.
        /// </summary>
        internal static string FilmMaxPk {
            get {
                return ResourceManager.GetString("FilmMaxPk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT *
        ///FROM Film
        ///WHERE Id = @Id.
        /// </summary>
        internal static string FilmSelectById {
            get {
                return ResourceManager.GetString("FilmSelectById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Film
        ///SET Title = @Title, Subtitle = @Subtitle, FormatId = @FormatId,  GenreId01 = @GenreId01, GenreId02 = @GenreId02, Synopsis = @Synopsis, DurationMin = @DurationMin, DownloadDate = @DownloadDate, PremiereDate = @PremiereDate, Note = @Note, Cover = @Cover, Background = @Background
        ///WHERE Id = @Id.
        /// </summary>
        internal static string FilmUpdate {
            get {
                return ResourceManager.GetString("FilmUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE Format WHERE Id = @Id
        ///.
        /// </summary>
        internal static string FormatDelete {
            get {
                return ResourceManager.GetString("FormatDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM Format.
        /// </summary>
        internal static string FormatFindAll {
            get {
                return ResourceManager.GetString("FormatFindAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Format (Name, Quality)
        ///VALUES(@Name, @Quality);
        ///SELECT @@IDENTITY;
        ///.
        /// </summary>
        internal static string FormatInsert {
            get {
                return ResourceManager.GetString("FormatInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ISNULL(MAX(Id), 0) FROM Format.
        /// </summary>
        internal static string FormatMaxPk {
            get {
                return ResourceManager.GetString("FormatMaxPk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT *
        ///FROM Format
        ///WHERE Id = @Id
        ///.
        /// </summary>
        internal static string FormatSelectById {
            get {
                return ResourceManager.GetString("FormatSelectById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Format
        ///SET Name = @Name, Quality = @Quality
        ///WHERE Id = @Id
        ///.
        /// </summary>
        internal static string FormatUpdate {
            get {
                return ResourceManager.GetString("FormatUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE Genre WHERE Id = @Id.
        /// </summary>
        internal static string GenreDelete {
            get {
                return ResourceManager.GetString("GenreDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM Genre.
        /// </summary>
        internal static string GenreFindAll {
            get {
                return ResourceManager.GetString("GenreFindAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Genre (Name)
        ///VALUES(@Name);
        ///SELECT @@IDENTITY;.
        /// </summary>
        internal static string GenreInsert {
            get {
                return ResourceManager.GetString("GenreInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ISNULL(MAX(Id), 0) FROM Genre.
        /// </summary>
        internal static string GenreMaxPk {
            get {
                return ResourceManager.GetString("GenreMaxPk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT *
        ///FROM Genre
        ///WHERE Id = @Id.
        /// </summary>
        internal static string GenreSelectById {
            get {
                return ResourceManager.GetString("GenreSelectById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Genre
        ///SET Name = @Name
        ///WHERE Id = @Id.
        /// </summary>
        internal static string GenreUpdate {
            get {
                return ResourceManager.GetString("GenreUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE Serie WHERE Id = @Id.
        /// </summary>
        internal static string SerieDelete {
            get {
                return ResourceManager.GetString("SerieDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM Serie.
        /// </summary>
        internal static string SerieFindAll {
            get {
                return ResourceManager.GetString("SerieFindAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Serie (Title, GenreId01, GenreId02, Synopsis, DurationMin, DownloadDate, PremiereDate, Note, Cover, Background)
        ///VALUES(@Title, @GenreId01, @GenreId02, @Synopsis, @DurationMin, @DownloadDate, @PremiereDate, @Note, @Cover, @Background);
        ///SELECT @@IDENTITY;.
        /// </summary>
        internal static string SerieInsert {
            get {
                return ResourceManager.GetString("SerieInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ISNULL(MAX(Id), 0) FROM Serie.
        /// </summary>
        internal static string SerieMaxPk {
            get {
                return ResourceManager.GetString("SerieMaxPk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT *
        ///FROM Serie
        ///WHERE Id = @Id.
        /// </summary>
        internal static string SerieSelectById {
            get {
                return ResourceManager.GetString("SerieSelectById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Serie
        ///SET Title = @Title, GenreId01 = @GenreId01, GenreId02 = @GenreId02, Synopsis = @Synopsis, DurationMin = @DurationMin, DownloadDate = @DownloadDate, PremiereDate = @PremiereDate, Note = @Note, Cover = @Cover, Background = @Background
        ///WHERE Id = @Id
        ///.
        /// </summary>
        internal static string SerieUpdate {
            get {
                return ResourceManager.GetString("SerieUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE UserInfo WHERE Id = @Id.
        /// </summary>
        internal static string UserDelete {
            get {
                return ResourceManager.GetString("UserDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM UserInfo.
        /// </summary>
        internal static string UserFindAll {
            get {
                return ResourceManager.GetString("UserFindAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM EpisodeSeen WHERE UserId = @Id.
        /// </summary>
        internal static string UserGetEpisodeSeen {
            get {
                return ResourceManager.GetString("UserGetEpisodeSeen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM UserEpisodeTime WHERE UserId = @Id.
        /// </summary>
        internal static string UserGetEpisodeTime {
            get {
                return ResourceManager.GetString("UserGetEpisodeTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM FilmSeen WHERE UserId = @Id.
        /// </summary>
        internal static string UserGetFilmSeen {
            get {
                return ResourceManager.GetString("UserGetFilmSeen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM UserFilmTime WHERE UserId = @Id.
        /// </summary>
        internal static string UserGetFilmTime {
            get {
                return ResourceManager.GetString("UserGetFilmTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM SerieSeen WHERE UserId = @Id.
        /// </summary>
        internal static string UserGetSerieSeen {
            get {
                return ResourceManager.GetString("UserGetSerieSeen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO UserInfo (Name, NickName, Password, Email, Bitmap, Permissions)
        ///VALUES (@Name, @NickName, @Password, @Email, @Bitmap, @Permissions);
        ///SELECT @@IDENTITY;
        ///.
        /// </summary>
        internal static string UserInsert {
            get {
                return ResourceManager.GetString("UserInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ISNULL(MAX(Id), 0) FROM UserInfo.
        /// </summary>
        internal static string UserMaxPk {
            get {
                return ResourceManager.GetString("UserMaxPk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT *
        ///FROM UserInfo
        ///WHERE Id = @Id.
        /// </summary>
        internal static string UserSelectById {
            get {
                return ResourceManager.GetString("UserSelectById", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE UserInfo
        ///SET Name = @Name, NickName = @NickName, Email = @Email, Password = @Password, Bitmap = @Bitmap, Permissions = @Permissions
        ///WHERE Id = @Id
        ///.
        /// </summary>
        internal static string UserUpdate {
            get {
                return ResourceManager.GetString("UserUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string UserUpdateEpisodeSeen {
            get {
                return ResourceManager.GetString("UserUpdateEpisodeSeen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string UserUpdateEpisodeTime {
            get {
                return ResourceManager.GetString("UserUpdateEpisodeTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string UserUpdateFilmSeen {
            get {
                return ResourceManager.GetString("UserUpdateFilmSeen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string UserUpdateFilmTime {
            get {
                return ResourceManager.GetString("UserUpdateFilmTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string UserUpdateSerieSeen {
            get {
                return ResourceManager.GetString("UserUpdateSerieSeen", resourceCulture);
            }
        }
    }
}

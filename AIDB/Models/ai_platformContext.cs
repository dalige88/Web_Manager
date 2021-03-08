using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AIDB.Models
{
    public partial class ai_platformContext : DbContext
    {
        public ai_platformContext()
        {
        }

        public ai_platformContext(DbContextOptions<ai_platformContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Jrttfensiinfo> Jrttfensiinfo { get; set; }
        public virtual DbSet<Jrttimagesinfo> Jrttimagesinfo { get; set; }
        public virtual DbSet<Jrttpinluninfo> Jrttpinluninfo { get; set; }
        public virtual DbSet<Jrttuserinfo> Jrttuserinfo { get; set; }
        public virtual DbSet<Jrttweitoutiaoinfo> Jrttweitoutiaoinfo { get; set; }
        public virtual DbSet<Jrttwenzhanginfo> Jrttwenzhanginfo { get; set; }
        public virtual DbSet<NewMirrorInfo> NewMirrorInfo { get; set; }
        public virtual DbSet<Platforminfo> Platforminfo { get; set; }
        public virtual DbSet<Postcontent> Postcontent { get; set; }
        public virtual DbSet<Postingrecord> Postingrecord { get; set; }
        public virtual DbSet<Subchannel> Subchannel { get; set; }
        public virtual DbSet<Usercommentlistinfo> Usercommentlistinfo { get; set; }
        public virtual DbSet<Usercommenttargetinfo> Usercommenttargetinfo { get; set; }
        public virtual DbSet<Videoyoutube> Videoyoutube { get; set; }
        public virtual DbSet<Ypjrttweitoutiaoinfo> Ypjrttweitoutiaoinfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=127.0.0.1;userid=root;pwd=root;port=3306;database=ai_platform;sslmode=none", x => x.ServerVersion("5.7.26-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jrttfensiinfo>(entity =>
            {
                entity.ToTable("jrttfensiinfo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AvatarUrl)
                    .HasColumnName("avatar_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.OpenUrl)
                    .HasColumnName("open_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ScreenName)
                    .HasColumnName("screen_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Jrttimagesinfo>(entity =>
            {
                entity.ToTable("jrttimagesinfo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Height)
                    .HasColumnName("height")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MimeType)
                    .HasColumnName("mime_type")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PlatforminfoId)
                    .HasColumnName("platforminfo_id")
                    .HasColumnType("bigint(20)")
                    .HasComment("平台ID（头条、百度。。。。）");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("varchar(255)")
                    .HasComment("本地URL")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.WebUrl)
                    .HasColumnName("web_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Width)
                    .HasColumnName("width")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Jrttpinluninfo>(entity =>
            {
                entity.ToTable("jrttpinluninfo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AvatarUrl)
                    .HasColumnName("avatar_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DongtaiId)
                    .HasColumnName("dongtai_id")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasColumnType("varchar(600)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Jrttuserinfo>(entity =>
            {
                entity.ToTable("jrttuserinfo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID_")
                    .HasColumnType("bigint(11)");

                entity.Property(e => e.AvatarUrl)
                    .HasColumnName("avatar_url")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BindMobile)
                    .HasColumnName("bind_mobile")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreatorId)
                    .HasColumnName("creator_id")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Id1)
                    .HasColumnName("id")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.LocationName)
                    .HasColumnName("location_name")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedAvatarUri)
                    .HasColumnName("verified_avatar_uri")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedDescription)
                    .HasColumnName("verified_description")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.VerifiedName)
                    .HasColumnName("verified_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Jrttweitoutiaoinfo>(entity =>
            {
                entity.ToTable("jrttweitoutiaoinfo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PublishTime)
                    .HasColumnName("publish_time")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ThreadId)
                    .HasColumnName("thread_id")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UgcU13CutImageList)
                    .HasColumnName("ugc_u13_cut_image_list")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Jrttwenzhanginfo>(entity =>
            {
                entity.ToTable("jrttwenzhanginfo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Abstract)
                    .HasColumnName("abstract")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ArticleGenre)
                    .HasColumnName("article_genre")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BehotTime)
                    .HasColumnName("behot_time")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ChineseTag)
                    .HasColumnName("chinese_tag")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CommentsCount)
                    .HasColumnName("comments_count")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Composition)
                    .HasColumnName("composition")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DetailPlayEffectiveCount)
                    .HasColumnName("detail_play_effective_count")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DisplayUrl)
                    .HasColumnName("display_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GallaryImageCount)
                    .HasColumnName("gallary_image_count")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GoDetailCount)
                    .HasColumnName("go_detail_count")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.GroupSource)
                    .HasColumnName("group_source")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HasGallery)
                    .HasColumnName("has_gallery")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HasVideo)
                    .HasColumnName("has_video")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ImageList)
                    .HasColumnName("image_list")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MediaUrl)
                    .HasColumnName("media_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MiddleMode)
                    .HasColumnName("middle_mode")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MoreMode)
                    .HasColumnName("more_mode")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SingleMode)
                    .HasColumnName("single_mode")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SourceUrl)
                    .HasColumnName("source_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Tag)
                    .HasColumnName("tag")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TagUrl)
                    .HasColumnName("tag_url")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Visibility)
                    .HasColumnName("visibility")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<NewMirrorInfo>(entity =>
            {
                entity.ToTable("new_mirror_info");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(10)");

                entity.Property(e => e.Urls)
                    .HasColumnName("urls")
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.YwTitle)
                    .HasColumnName("yw_title")
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ZwTitle)
                    .HasColumnName("zw_title")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Platforminfo>(entity =>
            {
                entity.ToTable("platforminfo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddressUrl)
                    .HasColumnName("AddressURL")
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.PlatformName)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Postcontent>(entity =>
            {
                entity.ToTable("postcontent");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreateManagerId)
                    .HasColumnName("CreateManagerID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateType).HasColumnType("int(255)");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("CreateUserID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CreateUserType).HasColumnType("int(255)");

                entity.Property(e => e.HeadImg)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasComment("头图  服务器  物理  地址")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HeadImgServer)
                    .HasColumnType("varchar(255)")
                    .HasComment("头图  服务器  浏览  地址")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MsgAuthor)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MsgContent)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MsgTitle)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MsgType).HasColumnType("int(255)");

                entity.Property(e => e.OpenStatus).HasColumnType("int(255)");

                entity.Property(e => e.PlatformIds)
                    .IsRequired()
                    .HasColumnName("PlatformIDs")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SubChannelId)
                    .HasColumnName("SubChannelID")
                    .HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<Postingrecord>(entity =>
            {
                entity.ToTable("postingrecord");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.ManagerId)
                    .HasColumnName("ManagerID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.MsgTitle)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PlatformId)
                    .HasColumnName("PlatformID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.PostContentId)
                    .HasColumnName("PostContentID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.PostData)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PostTime).HasColumnType("datetime");

                entity.Property(e => e.PostType).HasColumnType("int(11)");

                entity.Property(e => e.ReturnData)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SubChannelId)
                    .HasColumnName("SubChannelID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Success).HasColumnType("int(11) unsigned");
            });

            modelBuilder.Entity<Subchannel>(entity =>
            {
                entity.ToTable("subchannel");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AddressUrl)
                    .HasColumnName("AddressURL")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.AnalogPacket)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.ManagerId)
                    .HasColumnName("ManagerID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ManagerName)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PlatformId)
                    .HasColumnName("PlatformID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.PyscriptComment)
                    .HasColumnName("PYScript_Comment")
                    .HasColumnType("varchar(255)")
                    .HasComment("PY评论发布脚本（评论）")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PyscriptLongEssay)
                    .HasColumnName("PYScript_LongEssay")
                    .HasColumnType("varchar(255)")
                    .HasComment("PY长文发布脚本（文章）")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PyscriptPic)
                    .HasColumnName("PYScript_PIC")
                    .HasColumnType("varchar(255)")
                    .HasComment("PY图库发布脚本（图库：上传图片）")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PyscriptShortEssay)
                    .HasColumnName("PYScript_ShortEssay")
                    .HasColumnType("varchar(255)")
                    .HasComment("PY短文发布脚本（微头条）")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PyscriptVideo)
                    .HasColumnName("PYScript_Video")
                    .HasColumnType("varchar(255)")
                    .HasComment("PY视频发布脚本（视频）")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.States).HasColumnType("int(11)");

                entity.Property(e => e.SubChannelName)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserName)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserPwd)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Usercommentlistinfo>(entity =>
            {
                entity.ToTable("usercommentlistinfo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CommentContent)
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.CommentTargetId)
                    .HasColumnName("CommentTargetID")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CommentTime)
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DongtaiId)
                    .HasColumnName("dongtai_id")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ManagerId)
                    .HasColumnName("ManagerID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ManagerName)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ParentId)
                    .HasColumnName("ParentID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ReplyContent)
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ReplyTime)
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SignStatus).HasColumnType("int(11)");

                entity.Property(e => e.UserAccount)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UserNice)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Usercommenttargetinfo>(entity =>
            {
                entity.ToTable("usercommenttargetinfo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CommentTargetId)
                    .HasColumnName("CommentTargetID")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CommentTargetTitle)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CommentType).HasColumnType("int(11)");

                entity.Property(e => e.PlatformId)
                    .HasColumnName("PlatformID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Remark)
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SoureUrl)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Videoyoutube>(entity =>
            {
                entity.ToTable("videoyoutube");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Downloadstate)
                    .HasColumnName("downloadstate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Downloadtime)
                    .HasColumnName("downloadtime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Downloadurls)
                    .HasColumnName("downloadurls")
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Localsrc)
                    .HasColumnName("localsrc")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Poststate)
                    .HasColumnName("poststate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Posttime)
                    .HasColumnName("posttime")
                    .HasColumnType("datetime");

                entity.Property(e => e.YwTitle)
                    .HasColumnName("yw_title")
                    .HasColumnType("varchar(400)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ZwTitle)
                    .HasColumnName("zw_title")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Ypjrttweitoutiaoinfo>(entity =>
            {
                entity.ToTable("ypjrttweitoutiaoinfo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Createtime)
                    .HasColumnName("createtime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Images)
                    .HasColumnName("images")
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PlatformIds)
                    .HasColumnName("PlatformIDs")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SourceLink)
                    .HasColumnName("sourceLink")
                    .HasColumnType("varchar(255)")
                    .HasComment("源链接地址")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

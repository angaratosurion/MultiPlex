﻿using System;
using System.ComponentModel.DataAnnotations;
using BlackCogs.Data.Models;
using MultiPlex.Core.Data.Models;

namespace MultiPlex.Core.Data.ViewModels
{
    public class ViewWikiContent
    {
        public ViewWikiContent()
        {

        }
        //public Content(int id,Title title, string source, int version
        //   ,DateTime versionDate, Wiki wiki, ApplicationUser writtenBy)
        //{
        //    this.Id = id;
        //    this.Title = title;
        //    this.Source = source;
        //    this.Version = version;
        //    this.VersionDate = versionDate;
        //    this.Wiki = wiki;
        //    this.WrittenBy = writtenBy;
        //}
        [Required]
        public int Id { get; set; }
        [Required]
        public virtual WikiTitle Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Source { get; set; }
        public string RenderedSource { get; set; } 
        [Required]      
        public int Version { get; set; }
        [Required]
        [DataType(DataType.DateTime)]        
        public DateTime VersionDate { get; set; }
       
        [Required]
        public virtual Wiki Wiki { get; set; }
        [Required]        
        public virtual ApplicationUser WrittenBy { get; set; }
        public void ImportFromModel(WikiContent md)
        {
            try
            {
                if (md != null && CommonTools.isEmpty(md.WrittenBy))
                {
                    ApplicationUser user = CommonTools.usrmng.GetUserbyID(md.WrittenBy);
                    if (user != null)
                    {
                        this.Id = md.Id;
                        this.Source = md.Source;
                        this.Title = md.Title;
                        this.Version = md.Version;
                        this.RenderedSource = md.RenderedSource;
                        this.VersionDate = md.VersionDate;
                        this.Wiki = md.Wiki;
                        this.WrittenBy = user;
                       


                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }

    }
}
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web;
using Anita;

namespace WebApp.Pages
{
    public class EditModel : PageModel
    {
        public Album upRecord { get; set; }
        public IList<joinResult2> joinResults2 { get; set; }
        public IList<joinResult> joinResults { get; set; }
        public IList<Artist> artistss { get; set; }

        public void OnGet()
        {
            // Get the id  from the querysting, cast to an Integer and save it to a variable id
            int id = int.Parse(HttpContext.Request.Query["id"]);
            chinook db = new chinook();
            //get all artists to list
            artistss = db.artists.ToList();
            //find record where AlbumId = chosen record id
            upRecord = db.albums.First(c => c.AlbumId == id);

            //joining albums table with tracks table on AlbumId
            joinResults2 = db.albums
        .Join(
        db.tracks,
            alb => alb.AlbumId,
            tr => tr.AlbumId,
            (alb, tr) => new joinResult2()
            {
                TrackId = tr.TrackId,
                AlbumId = alb.AlbumId,
                AlbumName = alb.Title,
                TrackName = tr.Name
            }
            //show record where AlbumId = chosen record id
        ).Where(alb => alb.AlbumId == id).ToList();

            //joining albums table with artists table on ArtistId
            joinResults = db.artists
            .Join(
            db.albums,
                art => art.ArtistId,
                alb => alb.ArtistId,
                (art, alb) => new joinResult()
                {
                    ArtistId = art.ArtistId,
                    AlbumId = alb.AlbumId,
                    AlbumName = alb.Title,
                    ArtistName = art.Name
                }
            //show record where AlbumId = chosen record id
            ).Where(alb => alb.AlbumId == id).ToList();
        }

        public IActionResult OnPost()
        {
            chinook db = new chinook();
            // Get ID from html form where --> name="ID" value="@Model.upRecord.AlbumId"
            int ID = int.Parse(Request.Form["ID"]);
            //set upRecord so its id matches the id of chosen album so we can update it
            upRecord = db.albums.First(c => c.AlbumId == ID);
            // take information for an album update from user input
            string title = Request.Form["titlename"];
            int artysta = int.Parse(Request.Form["artysta"]);
            //pass previously taken form input info for album update as it's Title and ArtistId
            upRecord.Title = title;
            upRecord.ArtistId = artysta;

            // Update albums table and save changes
            db.albums.Update(upRecord);
            db.SaveChanges();
            //After successful record update go back to index page
            return Redirect("~/Index");
        }
    }
}



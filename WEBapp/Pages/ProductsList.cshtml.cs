using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Anita;


namespace WebApp.Pages
{
    public class ListModel : PageModel
    {
        public string Headingg { get; set; }
        public IList<joinResult> joinResults { get; set; }
        public IList<Album> albums { get; set; }
        public IList<Artist> artists { get; set; }
        public IList<Track> tracks { get; set; }
        public IList<joinResult2> joinResults2 { get; set; }

        public void OnGet()
        {
            // Get the id  from the querysting, cast to an Integer and save it to a variable id
            int id = System.Int32.Parse(HttpContext.Request.Query["id"]);

            Headingg = "Details:";

//joining albums table with tracks table on AlbumId
            chinook context = new chinook();
            joinResults2 = context.albums
                .Join(
                context.tracks,
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
            chinook context2 = new chinook();
            joinResults = context2.artists
                    .Join(
                    context2.albums,
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
    }
}
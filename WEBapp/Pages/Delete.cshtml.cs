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
    public class DelModel : PageModel
    {
        public Album remAlb { get; set; }
        public IList<Track> remTr { get; set; }
   
        public IList<joinResult> joinResults { get; set; }
        chinook db = new chinook();

        public void OnGet()
        {
            int id = int.Parse(HttpContext.Request.Query["id"]);

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
                    ).Where(alb => alb.AlbumId == id).ToList();

            remTr = db.tracks.Where(t => t.AlbumId == id).ToList();
            foreach (var trak in remTr){
                db.tracks.Remove(trak);
                 db.SaveChanges();
            }
            remAlb = db.albums.First(c => c.AlbumId == id);

            db.albums.Remove(remAlb);
            db.SaveChanges();
        }
    }
}




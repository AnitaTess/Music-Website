using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Anita;

namespace WebApp.Pages
{
    public class ProductsModel : PageModel
    {
        public string Heading { get; set; }
        public IList<joinResult> joinResults { get; set; }
        public IList<Artist> artists { get; set; }
        public IList<Album> albums { get; set; }
        public string SearchTerm { get; set; }

        public void OnGet(string SearchTerm)
        {
            Heading = "View, Update, Add and Delete albums here:";
            chinook context = new chinook();
            if (string.IsNullOrEmpty(SearchTerm))
            {
                //joining artists table with albums table on ArtistId so we can see artist name next to his album title on home page  
                joinResults = context.artists
                        .Join(
                        context.albums,
                            art => art.ArtistId,
                            alb => alb.ArtistId,
                            (art, alb) => new joinResult()
                            {
                                ArtistId = art.ArtistId,
                                AlbumId = alb.AlbumId,
                                AlbumName = alb.Title,
                                ArtistName = art.Name
                            }
                        //alphabetical order on artists names
                        ).OrderBy(Artist => Artist.ArtistName).ToList();
            }
            else
            {
                //joining artists table with albums table on ArtistId so we can see artist name next to his album title on home page
                joinResults = context.artists
        .Join(
        context.albums,
            art => art.ArtistId,
            alb => alb.ArtistId,
            (art, alb) => new joinResult()
            {
                ArtistId = art.ArtistId,
                AlbumId = alb.AlbumId,
                AlbumName = alb.Title,
                ArtistName = art.Name
            }
            //alphabetical order on artists names and find where title includes search input
            ).OrderBy(Artist => Artist.ArtistName).Where(e => e.AlbumName.Contains(SearchTerm)).ToList();
            }

        }

    }
}

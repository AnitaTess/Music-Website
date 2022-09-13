using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using Anita;

namespace WebApp.Pages
{
    public class AddModel : PageModel
    {
        public IList<Artist> artistss { get; set; }
         
public IList<Artist> artids { get; set; }
        public IList<Album> albums { get; set; }
        public IList<Track> tracks { get; set; }

        public void OnGet()
        {
            chinook context = new chinook();
            artistss = context.artists.ToList();
        }
        public IActionResult OnPost()
        {
            chinook context = new chinook();
             // take information for new album creation from user input
           string title = Request.Form["titlename"];
            int artysta = int.Parse(Request.Form["artysta"]);
            
            //pass previously taken from input info for new album as it's Title and ArtistId
            Album newAlbum = new Album
            {
                Title = title,
                ArtistId = artysta
            };
            // Insert new album into the albums table
            context.albums.Add(newAlbum);
            context.SaveChanges();

            albums = context.albums.ToList();

             // Get new Album Id
                    int newalbId = newAlbum.AlbumId;

                    // Get all the input on Track array
                    string[] trackList = Request.Form["song[]"];

             // Insert all input on Track array into Track database
                    foreach (var trk in trackList)
                    {
                        Track newTrack = new Track();
                        newTrack.AlbumId = newalbId;
                        newTrack.Name = trk;
                    // Default value set for below, as its currently not being ask in insert form and we dont work with those tables
                        newTrack.GenreId = 3;
                        newTrack.MediaTypeId = 3;
                        newTrack.Milliseconds = 375418;
                        newTrack.UnitPrice = 1;
                         context.tracks.AddAsync(newTrack);
                         context.SaveChangesAsync();
                    }
                    //After successful record insert go back to index page
                    return Redirect("~/Index");
           
        }
    }
}
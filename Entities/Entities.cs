using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anita
{
   
    public class  Album{
       
        public int AlbumId {get; set;}
        public string Title {get; set;}
        public int ArtistId {get; set;}
       

    }

    public class  Artist{
        
        public int ArtistId {get; set;}
        public string Name {get; set;}
        
        
    }

public class  Track{
        public int TrackId {get; set;}
        public string Name {get; set;}
        public int AlbumId {get; set;}
        public int GenreId {get; set;}
        
        public int MediaTypeId {get; set;}
        public int Milliseconds {get; set;}
public int UnitPrice {get; set;}
    }

    public class  joinResult{
        public int ArtistId {get; set;}
        public string AlbumName {get; set;}
        public string ArtistName {get; set;}
        public int AlbumId {get; set;}

    }

    public class joinResult2{
        public string AlbumName {get; set;}
         public int AlbumId {get; set;}
         public int TrackId {get; set;}
        public string TrackName {get; set;}
    }

public class SelectListItem{
        public int Value {get; set;}
         public string Text {get; set;}
}
}
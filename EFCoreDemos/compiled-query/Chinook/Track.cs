﻿namespace compiled_query.Chinook;

public class Track : BaseEntity
{
    public Track()
    {
        InvoiceLines = new HashSet<InvoiceLine>();
    }

    public string? Name { get; set; }
    public int AlbumId { get; set; }
    public int MediaTypeId { get; set; }
    public int GenreId { get; set; }
    public string? Composer { get; set; }
    public int Milliseconds { get; set; }
    public int Bytes { get; set; }
    public decimal UnitPrice { get; set; }
    public virtual Album? Album { get; set; }
    public virtual Genre? Genre { get; set; }
    public virtual MediaType? MediaType { get; set; }
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }

    public virtual ICollection<Playlist>? Playlists { get; set; }
}
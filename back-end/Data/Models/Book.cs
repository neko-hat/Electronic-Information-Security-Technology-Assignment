using System;
using System.Collections.Generic;

namespace back_end.Data.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public string? Isbn13 { get; set; }

    public int? LanguageId { get; set; }

    public int? NumPages { get; set; }

    public DateTime? PublicationDate { get; set; }

    public int? PublisherId { get; set; }

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
